using System;
using System.IO;
using SQLite;

namespace ChallengeMk2.DataBase
{
    public class DatabaseParameters
    {
        public const SQLiteOpenFlags dbFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public const string dbFileName = "ChallengeMk2DataBase.db3";
        public static string dbPath
        {
            get
            {
#if DEBUG
                //Desktop or Download folder path from PC or MacBook
                var folderPath = Xamarin.Essentials.FileSystem.AppDataDirectory;
#else
                //App path
                var folderPath = Xamarin.Essentials.FileSystem.AppDataDirectory;
#endif
                var totalPath = Path.Combine(folderPath, dbFileName);
                return totalPath;
            }
        }
    }
}
