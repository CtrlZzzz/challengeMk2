using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using SQLite;
using Xamarin.Forms;

namespace ChallengeMk2.DataBase
{
    public class SQLiteDataService : ILocalDataService
    {
        const SQLiteOpenFlags DbFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        const string DbFileName = "ChallengeMk2DataBase.db3";

        SQLiteAsyncConnection database;

        IMapperService mapper;

        public SQLiteDataService()
        {
            mapper = DependencyService.Get<IMapperService>();
        }



        //could this method be synchronous instead ? do local db creation take too much time ? 
        public async Task InitializeAsync()
        {
            if (database == null)
            {
                database = new SQLiteAsyncConnection(GetDbPath(), DbFlags);
                await database.CreateTableAsync<StarSystemDbItem>();
            }
        }
        public async Task<List<StarSystem>> GetAllAsync()
        {
            var dbData = await GetAllDbItemAsync();

            var convertedData = new List<StarSystem>();
            foreach (var item in dbData)
            {
                convertedData.Add(mapper.ConvertFromDb(item));
            }

            return convertedData;
        }
        public async Task<StarSystem> GetItemAsync(string name)
        {
            var data = await GetDbItemAsync(name);

            return mapper.ConvertFromDb(data);
        }
        public async Task<StarSystem> GetItemAsync(int id)
        {
            var data = await GetDbItemAsync(id);

            return mapper.ConvertFromDb(data);
        }
        public async Task SaveItemAsync(StarSystem starSystem)
        {
            await SaveDbItemAsync(mapper.ConvertToDbItem(starSystem));
        }
        public async Task ClearDbAsync()
        {
            await database?.DropTableAsync<StarSystemDbItem>();
            await database?.CreateTableAsync<StarSystemDbItem>();
        }


        async Task<List<StarSystemDbItem>> GetAllDbItemAsync()
        {
            return await database?.Table<StarSystemDbItem>().ToListAsync();
        }
        async Task<StarSystemDbItem> GetDbItemAsync(string name)
        {
            return await database?.Table<StarSystemDbItem>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public async Task<StarSystemDbItem> GetDbItemAsync(int id)
        {
            return await database?.Table<StarSystemDbItem>().Where(i => i.DbID == id).FirstOrDefaultAsync();
        }
        public async Task SaveDbItemAsync(StarSystemDbItem starSystem)
        {
            await database?.InsertAsync(starSystem);
        }

        string GetDbPath()
        {
            var folderPath = Xamarin.Essentials.FileSystem.AppDataDirectory;

            var totalPath = Path.Combine(folderPath, DbFileName);

            return totalPath;
        }
    }
}
