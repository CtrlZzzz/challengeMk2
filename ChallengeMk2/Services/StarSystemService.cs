using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.DataBase;
using ChallengeMk2.Models;
using Xamarin.Essentials;

namespace ChallengeMk2.Services
{
    class StarSystemService : IStarSystemService
    {
        ILocalDataService localService;
        IWebDataService webService;

        bool isOnLocalData;

        public StarSystemService(ILocalDataService localDataService, IWebDataService webDataService)
        {
            localService = localDataService;
            webService = webDataService;
        }



        public async Task<List<StarSystem>> GetStarSystemDataAsync()
        {
            //DEBUG
            Preferences.Remove("dbExpirationDate");

            if (Connectivity.NetworkAccess != NetworkAccess.Internet && localService.GetNullState())
            {
                //TODO : alert : no connection nor local data saved
            }

            await localService.InitializeAsync();

            var expirationString = Preferences.Get("dbExpirationDate", null);
            DateTime.TryParse(expirationString, out var expirationDate);

            if (Connectivity.NetworkAccess != NetworkAccess.Internet || (expirationString != null && DateTime.Now <= expirationDate))
            {
                isOnLocalData = true;

                return await localService.GetAllAsync();
            }
            else
            {
                isOnLocalData = false;

                var data = await webService.GetAllAsync();

                await SaveDataInDbAsync(data);

                return data;
            }
        }

        public bool GetLocalState()
        {
            return isOnLocalData;
        }

        async Task SaveDataInDbAsync(List<StarSystem> data)
        {
            await localService.ClearDbAsync();  //Maybe is it better to update items instead of delete and re-save them ?

            localService.SaveAll(data);

            Preferences.Set("dbExpirationDate", DateTime.Now.AddDays(7).ToString());
        }
    }
}
