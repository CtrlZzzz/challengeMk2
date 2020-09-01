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
        readonly SQLiteAsyncConnection debugDb;

        public Database()
        {
            db = new SQLiteAsyncConnection(DatabaseParameters.DbPath, DatabaseParameters.DbFlags);
            db.CreateTableAsync<StarSystem>();

            //DEBUG
            debugDb = new SQLiteAsyncConnection(DatabaseParameters.DbDebugPath, DatabaseParameters.DbFlags);
            debugDb.CreateTableAsync<StarSystem>();
        }

        public Task<List<StarSystem>> GetDbFullAsync()
        {
            return db.Table<StarSystem>().ToListAsync();
        }

        public Task<int> SaveDbItemAsync(StarSystem starSystem)
        {
            return db.InsertAsync(starSystem);
        }

        //DEBUG
        public Task<int> SaveDbDebugAsync(StarSystem starSystem)
        {
            return debugDb.InsertAsync(starSystem);
        }
    }
}
