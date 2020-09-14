using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.DataBase;
using ChallengeMk2.Models;
using ChallengeMk2.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    public class StarSystemsViewModel : BaseViewModel
    {
        const int SearchRadius = 99;

        public StarSystemsViewModel()
        {
            Title = "Systems around SOL (Api)";

            Systems = new ObservableCollection<StarSystem>();

            LoadSystemDataCommand = new Command(async () => await LoadSystemDataAsync());
        }

        StarSystem selectedSystem;
        public StarSystem SelectedSystem
        {
            set
            {
                SetProperty<StarSystem>(ref selectedSystem, value, onChanged: () =>
                {
                    NavigateTodetailPage(selectedSystem);
                    if (value != null)
                    {
                        selectedSystem = null;
                    }
                });
            }
        }

        public IList<StarSystem> Systems { get; set; }

        public Command LoadSystemDataCommand { get; set; }

        public NetworkAccess CurrentConnectivity { get; set; }

        internal Action<StarSystem> NavigateTodetailPage { get; set; }


        async Task LoadSystemDataAsync()
        {
            await InitializeDatabaseAsync();

            CurrentConnectivity = Connectivity.NetworkAccess;

            var expirationDate = Preferences.Get("dbExpirationDate", null);

            if (expirationDate != null && DateTime.Now <= DateTime.Parse(expirationDate))
            {
                await DisplaySavedDataAsync();
            }
            else
            {
                await DisplayApiDataAsync();
            }
        }

        async Task InitializeDatabaseAsync()
        {
            if (App.Database == null)
            {
                App.Database = new SQLiteDataService();
                await App.Database.InitializeAsync();
            }

            ////DEBUG
            //Preferences.Remove("dbExpirationDate");
        }

        async Task<List<StarSystem>> GetDataFromApiAsync()
        {
            using var client = new HttpClient();

            var url = $"https://www.edsm.net/api-v1/sphere-systems?showCoordinates=1&radius={SearchRadius}&showPermit=1&showInformation=1&showPrimaryStar=1";

            var response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<List<StarSystem>>(response);

            await SaveDataAsync(data);

            return data;
        }

        async Task SaveDataAsync(List<StarSystem> data)
        {
            await App.Database.ClearDbAsync();

            foreach (var system in data)
            {
                var dbItem = DatabaseMapper.ConvertToDbItem(system);
                await App.Database.SaveItemAsync(dbItem);
            }

            Preferences.Set("dbExpirationDate", DateTime.Now.AddDays(7).ToString());
            ////DEBUG
            //Preferences.Set("dbExpirationDate", DateTime.Now.AddSeconds(7).ToString());
        }

        async Task DisplaySavedDataAsync()
        {
            Title = "Systems around SOL (Local)";

            var localData = await App.Database.GetAllAsync();

            Systems.Clear();

            foreach (var system in localData)
            {
                var convertedSystem = DatabaseMapper.ConvertFromDb(system);

                Systems.Add(convertedSystem);
            }

            IsBusy = false;
        }

        async Task DisplayApiDataAsync()
        {
            Title = "Systems around SOL (Api)";

            IsBusy = true;

            try
            {
                var apiData = await GetDataFromApiAsync();

                Systems.Clear();

                foreach (var system in apiData)
                {
                    Systems.Add(system);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
