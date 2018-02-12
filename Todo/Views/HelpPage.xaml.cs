using Plugin.Messaging;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            hole.IsVisible = false;
        }

        public async void SendSMS_OnClicked(object sender, EventArgs e)
        {
            // Send SMS
            try
            {
                LogIn.checkSMSPermission();
                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                string smsToBeSent = (string)Application.Current.Properties["ToBeSend"];
                Debug.WriteLine("************" + smsToBeSent);
                if (smsMessenger.CanSendSmsInBackground)
                {
                    try
                    {
                        smsMessenger.SendSmsInBackground("7218185052", smsToBeSent);
                        hole.IsVisible = true;
                        messages.Text = "Successfully Sent!";

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
            }
            catch (Exception ex) {
                DisplayAlert("OOPS","You should give permission to the app to send SMS","OK");
            }
        }

        private void PhoneCall_Clicked(object sender, EventArgs e)
        {
            // Make Phone Call
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall("+27219333000");
        }

        private void SendEmail_Clicked(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            string emailToBeSent = (string)Application.Current.Properties["ToBeSend"];
            Debug.WriteLine("************" + emailToBeSent);
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                //emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

                // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                var email = new EmailMessageBuilder()
                  .To("to.plugins@xamarin.com")
                  .Cc("cc.plugins@xamarin.com")
                  .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                  .Subject("Help")
                  .Body(emailToBeSent)
                  .Build();

                emailMessenger.SendEmail(email);
                hole.IsVisible = false;
                Application.Current.Properties.Clear();
            }
        }
    }
}