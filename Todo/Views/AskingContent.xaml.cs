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
    public partial class AskingContent : ContentPage
    {
        public AskingContent()
        {
            InitializeComponent();

        }

        public async void Send_Button_Clicked(object sender, EventArgs e)
        {
            string textToBeSent = SendText.Text.Trim();
            if (textToBeSent.Length == 0)
            {
                await DisplayAlert("OOPS", "You cannot send empty text", "OK");
            }
            else {
                Application.Current.Properties["ToBeSend"] = textToBeSent;
                await Navigation.PushAsync(new HelpPage(), true);
                
            }
        }
    }
}