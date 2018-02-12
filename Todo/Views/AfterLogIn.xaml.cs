using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AfterLogIn : ContentPage
    {
        public AfterLogIn()
        {
            InitializeComponent();
        }

        public async void Get_Weather(Object sender, EventArgs ea)
        {
            await Navigation.PushAsync(new WeatherDetail(), true);
        }

        public async void Calculate_Area(Object sender, EventArgs ea)
        {
            await Navigation.PushAsync(new AreaCalculate(),true);
        }

        public async void Get_Help(Object sender, EventArgs ea)
        {
            Application.Current.Properties.Clear();
            LogIn.checkSMSPermission();
            await Navigation.PushAsync(new AskingContent(), true);
        }

    }
}