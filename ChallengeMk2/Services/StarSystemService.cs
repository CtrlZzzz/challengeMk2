using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeMk2.DataBase;
using ChallengeMk2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChallengeMk2.Services
{
    class StarSystemService : IStarSystemService
    {
        ILocalDataService localService;
        IWebDataService webService;

        bool isOnLocalData;

        public StarSystemService()
        {
            InitialeServices();
        }



        public async Task<List<StarSystem>> GetStarSystemDataAsync()
        {
            //DEBUG
            //Preferences.Remove("dbExpirationDate");

            if (Connectivity.NetworkAccess != NetworkAccess.Internet && localService.GetNullState())
            {
                //TODO : alert no connection nor local data saved
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


        void InitialeServices()
        {
            localService = DependencyService.Get<ILocalDataService>();
            webService = DependencyService.Get<IWebDataService>();
        }

        async Task SaveDataInDbAsync(List<StarSystem> data)
        {
            await localService.ClearDbAsync();

            localService.SaveAll(data);

            Preferences.Set("dbExpirationDate", DateTime.Now.AddDays(7).ToString());
        }
    }
}
