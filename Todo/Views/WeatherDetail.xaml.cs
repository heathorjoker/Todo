using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherDetail : ContentPage
    {
        public WeatherDetail()
        {
            InitializeComponent();
            initializeListView();
        }

        public async void initializeListView() {
            int x = await App.WDatabase.SaveItemAsync();
            if (x != -1)
            {
                List<Weather> listOfWeather = await App.WDatabase.GetWeekWeatherAsync();
                if (listOfWeather.Count > 0)
                {
                    WeatherView.ItemsSource = listOfWeather;
                    if (Application.Current.Properties.ContainsKey("Summary"))
                    {
                        summaryView.Text = (string)Application.Current.Properties["Summary"];
                    }
                }
                else {
                    await DisplayAlert("OOPS!", "You need to turn on the location and internet to get the data", "OK");
                }
            }
            else {
                if (Application.Current.Properties.ContainsKey("Location_ERR"))
                {
                    await DisplayAlert("OOPS!", (string)Application.Current.Properties["Location_ERR"], "OK");
                    Application.Current.Properties.Remove("Location_ERR");
                }
                else {
                    await DisplayAlert("OOPS!", "You need to turn on the location and internet to get the data", "OK");
                }
                
            }
            Application.Current.Properties.Clear();
        }

    }
}