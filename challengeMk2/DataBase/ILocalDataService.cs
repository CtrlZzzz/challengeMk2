using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface ILocalDataService
    {
        Task Initialize();

        Task<List<StarSystemDbItem>> GetFullDb();

        Task<StarSystemDbItem> GetItem(string name);
        Task<StarSystemDbItem> GetItem(int id);

        Task SaveItem(StarSystemDbItem starSystem);

        Task ClearDb();
    }
}
