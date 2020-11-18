using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface ILocalDataService
    {
        Task InitializeAsync();

        bool GetNullState();

        Task<List<StarSystem>> GetAllAsync();

        Task<StarSystem> GetItemAsync(string name);
        Task<StarSystem> GetItemAsync(int id);

        void SaveAll(List<StarSystem> systems);
        Task SaveItemAsync(StarSystem starSystem);

        Task ClearDbAsync();
    }
}
