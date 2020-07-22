using Repartidor.Interfaces;
using Repartidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace Repartidor.Data
{
    public class DataAccess  
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DataAccess()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(User)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<User> GetItemsAsync()
        {
            return Database.Table<User>().FirstOrDefaultAsync();
        }

        public Task<List<User>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<User>("SELECT * FROM User");
        }

      

        public Task<int> SaveItemAsync(User item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(User item)
        {
            return Database.DeleteAsync(item);
        }

    }

}
