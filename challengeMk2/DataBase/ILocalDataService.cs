using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.DataBase
{
    public interface ILocalDataService
    {
        Task InitializeAsync();

        Task<List<StarSystemDbItem>> GetAllAsync();

        Task<StarSystemDbItem> GetItemAsync(string name);
        Task<StarSystemDbItem> GetItemAsync(int id);

        Task SaveItemAsync(StarSystemDbItem starSystem);

        Task ClearDbAsync();
    }
}
