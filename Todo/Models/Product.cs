using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public string ProductSteps { get; set; }

        public int QuantityPerAcre { get; set; }

        public string cropType { get; set; }
    }
}
