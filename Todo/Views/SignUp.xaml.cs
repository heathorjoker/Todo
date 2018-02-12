using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Todo.Data;
using System.Text.RegularExpressions;
using Plugin.Connectivity;

namespace Todo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();
		}

        public async void onSignUpButtonClicked(object sender, EventArgs e)
        {
           
                    User user = new User();
                    user.Name = SignUpName.Text;
                    user.Email = SignUpEmail.Text;
                    user.Password = SignUpPassword.Text;
                    user.PhoneNo = SignUpPhoneNumber.Text;

                    user.ID = 0;
                    Boolean valid = true;
                    string message = "";
                    if (user.Name == null || user.Email == null || user.Password == null || user.PhoneNo == null)
                    {
                        valid = false;
                        message = "Fields cannot be empty";
                    }

                    if (valid)
                    {
                        //Phone number validation...
                        Regex phoneRegex = new Regex(@"^[0-9]{10}$");
                        Match phoneMatch = phoneRegex.Match(user.PhoneNo);
                        if (!phoneMatch.Success)
                        {
                            message += "\nPhone No must contain 10 digits";
                            valid = false;
                        }


                        //Email validation...
                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match match = regex.Match(user.Email);
                        if (!match.Success)
                        {
                            message += "\nInvalid Email Address";
                            valid = false;
                        }

                        //Password validation...
                        if (user.Password.Length < 8)
                        {
                            message += "\nLength of password should be greater than 7";
                            valid = false;
                        }

                        //Username validation...
                        if (user.Name.Length >= 4)
                        {
                            Regex regexName = new Regex(@"^(\w+[0-9]*)$");
                            Match matchName = regexName.Match(user.Name);
                            if (!matchName.Success)
                            {
                                message += "\nUsername should start with letter";
                                valid = false;
                            }  
                        }
                        else
                        {
                            message += "\nLength of username should be greater than 3";
                            valid = false;
                        }

                    }


                    if (valid)
                    {
                        if (user.Password.Equals(SignUpConfirmPassword.Text))
                        {

                            await App.Database.SaveItemAsync(user);
                            DisplayAlert("YOOO!!", "Succssfully Signed Up", "OK");
                        }
                        else
                        {
                            DisplayAlert("OOPS!!", "Error in Signing Up", "OK");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("*********************************here");
                        DisplayAlert("OOPS!!", message, "OK");
                    }
        }
    }
}