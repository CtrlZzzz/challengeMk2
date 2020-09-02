using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using SQLite;

namespace ChallengeMk2.DataBase
{
    public class SQLiteDataService : ILocalDataService
    {
        SQLiteConnection database;

        public void Initialize()
        {
            if (database == null)
            {
                database = new SQLiteConnection(DatabaseParameters.DbPath, DatabaseParameters.DbFlags);
                database.CreateTable<StarSystemDbItem>();
            }
        }

        public List<StarSystemDbItem> GetFullDb()
        {
            return database.Table<StarSystemDbItem>().ToList();
        }

        public StarSystemDbItem GetItem(string name)
        {
            return database.Table<StarSystemDbItem>().Where(i => i.Name == name).FirstOrDefault();
        }

        public StarSystemDbItem GetItem(int id)
        {
            return database.Table<StarSystemDbItem>().Where(i => i.DbID == id).FirstOrDefault();
        }

        public void SaveItem(StarSystemDbItem starSystem)
        {
            database.Insert(starSystem);
        }

        public void ClearDb()
        {
            database.DeleteAll<StarSystemDbItem>();
        }
    }
}
