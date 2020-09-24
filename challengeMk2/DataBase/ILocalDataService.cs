using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface ILocalDataService
    {
        Task InitializeAsync();

        Task<List<StarSystem>> GetAllAsync();

        Task<StarSystem> GetItemAsync(string name);
        Task<StarSystem> GetItemAsync(int id);

        Task SaveItemAsync(StarSystem starSystem);

        Task ClearDbAsync();
    }
}
