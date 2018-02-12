using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class Weather
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Summary { get; set; }

        public long Time { get; set; }

        public string Icon { get; set; }

        public int TemperatureLow { get; set; }

        public int TemperatureMax { get; set; }

        public string dateToDisplay { get; set; }

        public string SummaryOfWeek { get; set; }

        public string ToString
        {
            get
            {
                return "ID -> " + ID + "\nSummary -> " + Summary + " \n " + "Time -> " + Time; 
            }
        }
    }
}
