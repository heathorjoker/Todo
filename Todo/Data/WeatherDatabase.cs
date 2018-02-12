using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Views;
using Xamarin.Forms;

namespace Todo.Data
{
    public class WeatherDatabase
    {
        readonly SQLiteAsyncConnection database;
        public WeatherDatabase(string dbpath)
        {
            // SQLitePCL.raw.SetProvider(new SQLitePCL.Batteries.Init());
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<Weather>().Wait();

        }

        public Task<List<Weather>> GetWeekWeatherAsync()
        {
            return database.Table<Weather>().ToListAsync();
        }


        public Task<Weather> GetWeatherAsync(int ID)
        {
            return database.Table<Weather>().Where(i=>i.ID == ID).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync()
        {

            //Getting the current timestamp...
            //long currentTimeStamp = ConvertToTimestamp(DateTime.Now);
            Application.Current.Properties.Clear();
            await LogIn.checkLocationAsync();
            int count = await database.Table<Weather>().CountAsync();
            if (!Application.Current.Properties.ContainsKey("Location_ERR") && Application.Current.Properties.ContainsKey("latitude") && Application.Current.Properties.ContainsKey("longitude"))
            {
                double latitude = (double)Application.Current.Properties["latitude"];
                double longitude = (double)Application.Current.Properties["longitude"];
                //Building the queryString
                string queryString = "https://api.darksky.net/forecast/7066a9da455e545aff23508adb3c59f8/" + latitude + "," + longitude;
                //Get the data of the week..
                if (LogIn.checkConnectivity())
                {
                    var results = await DataCore.getDataFromService(queryString).ConfigureAwait(false);
                    Debug.WriteLine("Got data");
                    await DeleteItemAsync();

                    // Code for addition...
                    Application.Current.Properties["Summary"] = ((string)results["daily"]["summary"]).Split(',')[0];
                    if (results["daily"] != null)
                    {
                        var dailyData = results["daily"]["data"];
                        Debug.WriteLine(dailyData.Count() + " <- Count");
                        for (int i = 0; i < dailyData.Count() - 1; i++)
                        {
                            Weather weather = new Weather();
                            weather.Icon = (string)dailyData[i]["icon"];
                            if (weather.Icon.Equals("partly-cloudy"))
                            {
                                weather.Icon = "partly_cloudy";
                            }
                            else if (weather.Icon.Equals("clear-day"))
                            {
                                weather.Icon = "clear_day";
                            }
                            else if (weather.Icon.Equals("clear-night"))
                            {
                                weather.Icon = "clear_night";
                            }
                            else if (weather.Icon.Equals("cloudy-night"))
                            {
                                weather.Icon = "cloudy_night";
                            }
                            else if (weather.Icon.Equals("partly-cloudy-day"))
                            {
                                weather.Icon = "partly_cloudy";
                            }
                            else if (weather.Icon.Equals("partly-cloudy-night"))
                            {
                                weather.Icon = "cloudy_night";
                            }
                            weather.Icon += ".png";
                            weather.SummaryOfWeek = ((string)results["daily"]["summary"]).Split(',')[0]; 
                            Debug.WriteLine(weather.Icon);
                            weather.Summary = (string)dailyData[i]["summary"];
                            weather.Time = (long)dailyData[i]["time"];
                            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(weather.Time).ToLocalTime();
                            if (i > 0)
                            {
                                weather.dateToDisplay = dt.Date.DayOfWeek.ToString();
                            }
                            else if (i == 0)
                            {
                                weather.dateToDisplay = "Today";
                            }
                            weather.TemperatureLow = (int)(((float)dailyData[i]["temperatureMin"] - 32) * 5 / 9);
                            weather.TemperatureMax = (int)(((float)dailyData[i]["temperatureMax"] - 32) * 5 / 9);
                            await database.InsertAsync(weather);
                            Debug.WriteLine("Inserted!!");
                            Present.IsPresent = true;
                        }
                    }

                    return 1;
                }
                else
                {
                     DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ConvertToTimestamp(DateTime.Now)).ToLocalTime();
                     string currentWeekDay = dt.Date.DayOfWeek.ToString();
                     Weather weather = await database.Table<Weather>().Where(i => i.dateToDisplay == currentWeekDay).FirstAsync();
                     if (weather != null)
                     { 
                         Debug.WriteLine("Weather ID -> " + weather.ID);
                         await DeleteItemAsync(weather.ID);
                     }
                     else {
                         if (count == 0)
                         {
                             return -1;
                         }
                         Weather weather1 = await database.Table<Weather>().FirstAsync();
                         Application.Current.Properties["Summary"] = weather1.SummaryOfWeek;
                     } 
                }
                return 0;
            }
            else
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ConvertToTimestamp(DateTime.Now)).ToLocalTime();
                string currentWeekDay = dt.Date.DayOfWeek.ToString();
                Weather weather = await database.Table<Weather>().Where(i => i.dateToDisplay == currentWeekDay).FirstOrDefaultAsync();
                if (weather != null)
                {
                    Debug.WriteLine("Weather ID -> " + weather.ID);
                    await DeleteItemAsync(weather.ID);
                }
                if (count == 0)
                {
                    return -1;
                }
                Weather weather1 = await database.Table<Weather>().FirstAsync();
                Application.Current.Properties["Summary"] = weather1.SummaryOfWeek;
                return 0;
            }
        }

        public async Task<int> DeleteItemAsync()
        {
            int count = await database.Table<Weather>().CountAsync();
            if (count != 0) {
                Weather firstWeather = await database.Table<Weather>().FirstAsync();
                int id = firstWeather.ID + count;
                Debug.WriteLine("Last ID = > " + id);
                for (int i = firstWeather.ID; i < id; i++)
                {
                    Weather weather = await GetWeatherAsync(i);
                    if (weather != null)
                    {
                        await database.DeleteAsync(weather);
                    }
                    else {
                        break;
                    }
                }
            }
            return 1;
        }


        public async Task<int> DeleteItemAsync(int id)
        {
            int count = await database.Table<Weather>().CountAsync();
            if (count != 0)
            {
                Weather firstWeather = await database.Table<Weather>().FirstAsync();
                Debug.WriteLine("First Weather ID -> " + firstWeather.ID);
                for (int i = firstWeather.ID; i < (id); i++)
                {
                    Weather weather = await GetWeatherAsync(i);
                    await database.DeleteAsync(weather);
                }
                Weather firstWeather1 = await database.Table<Weather>().FirstAsync();
                firstWeather1.dateToDisplay = "Today";
                if (await (database.UpdateAsync(firstWeather1)) > 0) {
                    Debug.WriteLine("Updated!!");
                }
            }
            return 1;
        }


        private long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
       
    }
}
