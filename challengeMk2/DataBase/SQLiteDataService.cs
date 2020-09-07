using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using SQLite;

namespace ChallengeMk2.DataBase
{
    public class SQLiteDataService : ILocalDataService
    {
        SQLiteAsyncConnection database;

        public async Task Initialize()
        {
            if (database == null)
            {
                database = new SQLiteAsyncConnection(DatabaseParameters.DbPath, DatabaseParameters.DbFlags);
                await database.CreateTableAsync<StarSystemDbItem>();
            }
        }

        public async Task<List<StarSystemDbItem>> GetFullDb()
        {
            return await database.Table<StarSystemDbItem>().ToListAsync();
        }

        public async Task<StarSystemDbItem> GetItem(string name)
        {
            return await database.Table<StarSystemDbItem>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task<StarSystemDbItem> GetItem(int id)
        {
            return await database.Table<StarSystemDbItem>().Where(i => i.DbID == id).FirstOrDefaultAsync();
        }

        public async Task SaveItem(StarSystemDbItem starSystem)
        {
            await database.InsertAsync(starSystem);
        }

        public async Task ClearDb()
        {
            await database.DropTableAsync<StarSystemDbItem>();
            await database.CreateTableAsync<StarSystemDbItem>();
        }
    }
}
