using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Data
{
    public class ProductDatabase
    {
        readonly SQLiteAsyncConnection database;
        public ProductDatabase(string dbpath)
        {
            // SQLitePCL.raw.SetProvider(new SQLitePCL.Batteries.Init());
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProductAsync(string cropType)
        {
            //need to change the query here...
            return database.Table<Product>().Where(i => i.cropType == cropType).ToListAsync();
        }

        public Task<List<Product>> GetProductsAsync() {
            return database.Table<Product>().ToListAsync();
        }
 
        public Task<int> SaveItemAsync(Product item)
        {

            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }

        }

        public Task<int> DeleteItemAsync(Product item)
        {
            return database.DeleteAsync(item);
        }



    }
}
