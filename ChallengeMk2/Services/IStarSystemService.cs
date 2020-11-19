using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.Services
{
    public interface IStarSystemService
    {
        Task<List<StarSystem>> GetStarSystemDataAsync();

        bool GetLocalState();
    }
}
