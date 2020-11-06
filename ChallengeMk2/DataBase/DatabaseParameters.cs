using System;
using System.IO;
using SQLite;

namespace ChallengeMk2.DataBase
{
    public class DatabaseParameters
    {
        public const SQLiteOpenFlags DbFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public const string DbFileName = "ChallengeMk2DataBase.db3";

        public static string DbPath
        {
            get
            {
                //Application folder path 
                var folderPath = Xamarin.Essentials.FileSystem.AppDataDirectory;

                var totalPath = Path.Combine(folderPath, DbFileName);

                return totalPath;
            }
        }

        public static string DbDebugPath
        {
            get
            {
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                var totalPath = Path.Combine(folderPath, DbFileName);

                return totalPath;
            }
        }
    }
}
