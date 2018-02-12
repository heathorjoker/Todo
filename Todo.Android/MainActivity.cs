using Android.App;
using Android.OS;
using Android.Content.PM;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Todo
{
	[Activity(Label = "Todo", Icon = "@drawable/icon", MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity :
	global::Xamarin.Forms.Platform.Android.FormsApplicationActivity // superclass new in 1.3
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}


        //PDF CREATION IN ANDROID...
        public void createPDF(string inputText) {
            var directory = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory, "pdfTest").ToString();
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var path = Path.Combine(directory, "myTest.pdf");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            var fs = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.Add(new Paragraph(inputText));
            document.Close();
            writer.Close();
            fs.Close();
        }
        

    }


    
}
