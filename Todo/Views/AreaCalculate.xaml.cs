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
	public partial class AreaCalculate : ContentPage
	{
        private int count = 1;
        private List<Vertice> listOfVertices = new List<Vertice>();
        public AreaCalculate ()
		{
			InitializeComponent ();
            Done.IsVisible = false;
            Area_Display.IsVisible = false;
		}

        private async void Add_Point_Clicked(object sender, EventArgs e)
        {
            Vertice vertice = await LogIn.getCurrentLocation();

            if (count >= 3) {
                Done.IsVisible = true;
            }

            if (vertice != null)
            {
                count++;
                listOfVertices.Add(vertice);
            }
            else {
                await DisplayAlert("OOPS","Unable to get area.\nMake Sure location is turned on.","OK");    
            }
        }

        private async void Done_Clicked(object sender, EventArgs e)
        {
            if (count < 3)
            {
                await DisplayAlert("OOPS", "You need at least 3 points to get area", "ok");
            }
            else {
                Add.IsVisible = false;
                Area_Display.IsVisible = true;
                listOfVertices.Add(listOfVertices[0]);

                double area = Math.Abs(listOfVertices.Take(listOfVertices.Count - 1)
                   .Select((p, i) => (listOfVertices[i + 1].x - p.x) * (listOfVertices[i + 1].y + p.y))
                   .Sum() / 2);
                string x = "Area => " + area + "\n";
                for (int i = 0; i < listOfVertices.Capacity; i++) {
                    x += i + " => x ." + listOfVertices[i].x + "\n";
                    x += i + " => y. " + listOfVertices[i].y + "\n";
                }
                double areaInAcre = area * 0.000247105;
                Area_Display.Text = x;
            }

           
        }
    }
}