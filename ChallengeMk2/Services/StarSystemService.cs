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


        public StarSystemService()
        {
            InitialeServices();
        }



        public async Task<List<StarSystem>> GetStarSystemData()
        {
            //DEBUG
            Preferences.Remove("dbExpirationDate");

            if (Connectivity.NetworkAccess != NetworkAccess.Internet && localService.GetNullState())
            {
                //TODO : alert no connection and no local data saved
            }

            await localService.InitializeAsync();

            var expirationString = Preferences.Get("dbExpirationDate", null);
            DateTime.TryParse(expirationString, out var expirationDate);

            if (Connectivity.NetworkAccess != NetworkAccess.Internet || (expirationString != null && DateTime.Now <= expirationDate))
            {
                //LOCAL
                return await localService.GetAllAsync();
            }
            else
            {
                //API
                var data = await webService.GetAllAsync();

                await SaveDataInDb(data);

                return data;
            }
        }



        void InitialeServices()
        {
            localService = DependencyService.Get<ILocalDataService>();
            webService = DependencyService.Get<IWebDataService>();
        }

        async Task SaveDataInDb(List<StarSystem> data)
        {
            await localService.ClearDbAsync();

            foreach (var item in data)
            {
                //TODO : Bulk instead
                await localService.SaveItemAsync(item);
            }

            Preferences.Set("dbExpirationDate", DateTime.Now.AddDays(7).ToString());
        }
    }
}
