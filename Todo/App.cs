using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Todo.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Todo
{
	public class App : Application
	{
		static UserDatabase database = null;
        static WeatherDatabase wdatabase = null;
        static ProductDatabase pdatabase = null;

		public App()
		{
			/*Resources = new ResourceDictionary();
			Resources.Add("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));
            */
			MainPage = new NavigationPage(new Todo.Views.LogIn());
		}

    

		public static UserDatabase Database
		{
			get
			{
				if (database == null)
				{
                    try
                    {
                        database = new UserDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserSQLite.db3"));
                    }
                    catch (Exception e) {
                        throw e;
                    }
				}
				return database;
			}
		}

        public static WeatherDatabase WDatabase
        {
            get
            {
                if (wdatabase == null)
                {
                    try
                    {
                        wdatabase = new WeatherDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("WeatherSQLite.db3"));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return wdatabase;
            }
        }

        public static ProductDatabase PDatabase
        {
            get
            {
                if (pdatabase == null)
                {
                    try
                    {
                        pdatabase = new ProductDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ProductSQLite.db3"));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return pdatabase;
            }
        }

        public int ResumeAtTodoId { get; set; }

		protected override void OnStart()
		{
            //Debug.WriteLine("OnStart");

            //// always re-set when the app starts
            //// users expect this (usually)
            ////			Properties ["ResumeAtTodoId"] = "";
            //if (Properties.ContainsKey("ResumeAtTodoId"))
            //{
            //	var rati = Properties["ResumeAtTodoId"].ToString();
            //	Debug.WriteLine("   rati=" + rati);
            //	if (!String.IsNullOrEmpty(rati))
            //	{
            //		Debug.WriteLine("   rati=" + rati);
            //		ResumeAtTodoId = int.Parse(rati);

            //		if (ResumeAtTodoId >= 0)
            //		{
            //			var todoPage = new TodoItemPage();
            //			todoPage.BindingContext = await Database.GetItemAsync(ResumeAtTodoId);
            //			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
            //		}
            //	}
            //}
           // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_bait());
        }

		protected override void OnSleep()
		{
			//Debug.WriteLine("OnSleep saving ResumeAtTodoId = " + ResumeAtTodoId);
			//// the app should keep updating this value, to
			//// keep the "state" in case of a sleep/resume
			//Properties["ResumeAtTodoId"] = ResumeAtTodoId;
		}

		protected override void OnResume()
		{
			//Debug.WriteLine("OnResume");
			//if (Properties.ContainsKey("ResumeAtTodoId"))
			//{
			//	var rati = Properties["ResumeAtTodoId"].ToString();
			//	Debug.WriteLine("   rati=" + rati);
			//	if (!String.IsNullOrEmpty(rati))
			//	{
			//		Debug.WriteLine("   rati=" + rati);
			//		ResumeAtTodoId = int.Parse(rati);

			//		if (ResumeAtTodoId >= 0)
			//		{
			//			var todoPage = new TodoItemPage();
			//			todoPage.BindingContext = await Database.GetItemAsync(ResumeAtTodoId);
			//			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
			//		}
			//	}
			//}
		}
	}
}

