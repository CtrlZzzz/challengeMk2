using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using SQLite;

namespace ChallengeMk2.DataBase
{
    public class SQLiteDataService : ILocalDataService
    {
        SQLiteOpenFlags dbFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        string dbFileName = "ChallengeMk2DataBase.db3";

        SQLiteAsyncConnection database;

        public async Task InitializeAsync()
        {
            if (database == null)
            {
                database = new SQLiteAsyncConnection(GetDbPath(), dbFlags);
                await database.CreateTableAsync<StarSystemDbItem>();
            }
        }

        public async Task<List<StarSystemDbItem>> GetAllAsync()
        {
            return await database?.Table<StarSystemDbItem>().ToListAsync();
        }

        public async Task<StarSystemDbItem> GetItemAsync(string name)
        {
            return await database?.Table<StarSystemDbItem>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task<StarSystemDbItem> GetItemAsync(int id)
        {
            return await database?.Table<StarSystemDbItem>().Where(i => i.DbID == id).FirstOrDefaultAsync();
        }

        public async Task SaveItemAsync(StarSystemDbItem starSystem)
        {
            await database?.InsertAsync(starSystem);
        }

        public async Task ClearDbAsync()
        {
            await database?.DropTableAsync<StarSystemDbItem>();
            await database?.CreateTableAsync<StarSystemDbItem>();
        }

        string GetDbPath()
        {
            var folderPath = Xamarin.Essentials.FileSystem.AppDataDirectory;

            var totalPath = Path.Combine(folderPath, dbFileName);

            return totalPath;
        }
    }
}
