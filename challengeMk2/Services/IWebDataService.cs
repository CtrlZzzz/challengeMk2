using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.Services
{
    interface IWebDataService
    {
        Task<List<StarSystem>> GetAllAsync();

        Task<StarSystem> GetDetails(string systemName);
    }
}
