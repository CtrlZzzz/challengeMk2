using System;
using SQLite;
using ChallengeMk2.DataBase;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using System.Collections.Generic;

namespace ChallengeMk2.DataBase
{
    public class Database
    {
        readonly SQLiteAsyncConnection db;

        public Database()
        {
            db = new SQLiteAsyncConnection(DatabaseParameters.dbPath, DatabaseParameters.dbFlags);
            db.CreateTableAsync<StarSystem>();
        }

        public Task<List<StarSystem>> GetStarSystemsAsync()
        {
            return db.Table<StarSystem>().ToListAsync();
        }

        public Task<int> SaveStarSystemAsync(StarSystem starSystem)
        {
            return db.InsertAsync(starSystem);
        }
    }
}
