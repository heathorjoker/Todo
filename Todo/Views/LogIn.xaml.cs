using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn : ContentPage
	{
		public LogIn ()
		{
           
			InitializeComponent ();
            FarmyLogo.Source = ImageSource.FromFile("farmy_logo.png");
            Utils.CheckPermissions(Permission.Location);
            checkSMSPermission();
        }


        //Checking internet connection..
        public static Boolean checkConnectivity() {
            return CrossConnectivity.Current.IsConnected;
        }

        public static async void checkPermission() {
            await Utils.CheckPermissions(Permission.Location);
           
        }

        public static async void checkSMSPermission()
        {
            await Utils.CheckPermissions(Permission.Sms);
        }
      
        public static async Task checkLocationAsync() {

            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                var locator = CrossGeolocator.Current;

                var position = await locator.GetLastKnownLocationAsync();

                if (position == null)
                {
                    Application.Current.Properties["Location_ERR"] = "Couldn't get the location";
                }
                else {
                    double latitude = position.Latitude;
                    Application.Current.Properties["latitude"] = latitude;
                    double longitude = position.Longitude;
                    Application.Current.Properties["longitude"] = longitude;
                }
            }
            catch (Exception ex)
            {
                Application.Current.Properties["Location_ERR"] = "Something went wrong, but don't worry we captured for analysis! Thanks";
            }
        }



        public static async Task<Vertice> getCurrentLocation() {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission) {
                    return null;
                }

                var locator = CrossGeolocator.Current;

                var position = await locator.GetLastKnownLocationAsync();

                if (position == null)
                {
                    Application.Current.Properties["Location_ERR"] = "Couldn't get the location";
                    return null;
                }
                else
                {

                    double latitude = position.Latitude;
                    Application.Current.Properties["latitude"] = latitude;
                    double longitude = position.Longitude;
                    Application.Current.Properties["longitude"] = longitude;
                   /* double cosLatValue = Math.Cos(latitude);
                    double sinLongValue = Math.Sin(longitude);
                    double cosLongValue = Math.Sin(longitude);
                    double sinLatValue = Math.Sin(latitude);
                    double x = 6371 * 1000 * cosLatValue * cosLongValue;
                    double y = 6371 * 1000 * cosLatValue * sinLongValue;*/
                    Vertice v = new Vertice();
                    v.x = longitude;
                    v.y = latitude;
                    return v;
                }
            }
            catch (Exception ex)
            {
                Application.Current.Properties["Location_ERR"] = "Something went wrong, but don't worry we captured for analysis! Thanks";
                return null;
            }
        }    
        

        public async void SignUp_Clicked(Object sender, EventArgs ea)
        {
            await Navigation.PushAsync(new SignUp(), true);
        }


        public async void onLogInClicked(object sender, EventArgs e)
        {

           
                string email = LogInEmail.Text;
                string password = LogInPassword.Text;
                User user = await App.Database.GetItemAsync(email, password);
                if (user != null)
                {
                    await Navigation.PushAsync(new AfterLogIn(), true);
                }
                else
                {
                    await DisplayAlert("OOPS!!", "User Doesn't Exist", "OK");
                }
                
        }

    }
}