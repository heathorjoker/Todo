using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;
using System.Diagnostics;
using Xamarin.Forms;

namespace Todo.Data
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection database;
        public UserDatabase(string dbpath)
        {
           // SQLitePCL.raw.SetProvider(new SQLitePCL.Batteries.Init());
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<User>().Wait();
            
        }

        public Task<User> GetItemAsync(string email, string password)
        {
            return database.Table<User>().Where(i => i.Email == email && i.Password == password).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(User item) 
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

        public Task<int> DeleteItemAsync(User item)
        {
            return database.DeleteAsync(item);
        }


    }
}
