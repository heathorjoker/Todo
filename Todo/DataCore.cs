using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Todo
{
    public class DataCore
    {
        //Getting weather resonponse using HTTP request...
        public static async Task<JContainer> getDataFromService(string queryString)
        {           
                // String queryString = "https://api.darksky.net/forecast/7066a9da455e545aff23508adb3c59f8/"+ latitude+","+longitude;
                HttpClient client = new HttpClient();
            JContainer data = null;
            var response = await client.GetAsync(queryString);

              
                if (response != null)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(json);
                    data = (JContainer)JsonConvert.DeserializeObject(json);
                }

                return data;
        }
    }
}
