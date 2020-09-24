using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.DataBase;
using ChallengeMk2.Models;
using ChallengeMk2.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    public class StarSystemsViewModel : BaseViewModel
    {
        const int SearchRadius = 30;

        ILocalDataService localService;
        IWebDataService webService;
        IStarSystemService systemService;

        public StarSystemsViewModel()
        {
            InitializeViewModel();
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

        public Command DisplaySystemDataCommand { get; set; }

        internal Action<StarSystem> NavigateTodetailPage { get; set; }


        void InitializeViewModel()
        {
            GetServices();

            Title = "Systems around SOL (Api)";

            Systems = new ObservableCollection<StarSystem>();

            DisplaySystemDataCommand = new Command(async () => await DisplaySystemDataAsync());
        }

        void GetServices()
        {
            localService = DependencyService.Get<ILocalDataService>();
            webService = DependencyService.Get<IWebDataService>();
            systemService = DependencyService.Get<IStarSystemService>();
        }

        async Task DisplaySystemDataAsync()
        {
            IsBusy = true;
;
            try
            {
                Systems = await systemService.GetStarSystemData();
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

        //async Task InitializeDatabaseAsync()
        //{
        //    //if (App.Database == null)
        //    //{
        //    //    App.Database = new SQLiteDataService();
        //    //    await App.Database.InitializeAsync();
        //    //}

        //    await localService.InitializeAsync();

        //    //DEBUG
        //    Preferences.Remove("dbExpirationDate");
        //}

        //async Task DisplaySavedDataAsync()
        //{
        //    Title = "Systems around SOL (Local)";

        //    //var localData = await App.Database.GetAllAsync();
        //    //var localData = await localService.GetAllAsync();

        //    Systems.Clear();

        //    //foreach (var system in localData)
        //    //{
        //    //    Systems.Add(system);
        //    //}

        //    Systems = await localService.GetAllAsync();

        //    IsBusy = false;
        //}

        //async Task DisplayApiDataAsync()
        //{
        //    Title = "Systems around SOL (Api)";

        //    IsBusy = true;

        //    try
        //    {
        //        var apiData = await GetDataFromApiAsync();

        //        Systems.Clear();

        //        foreach (var system in apiData)
        //        {
        //            Systems.Add(system);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //async Task<List<StarSystem>> GetDataFromApiAsync()
        //{
        //    //using var client = new HttpClient();

        //    //var url = $"https://www.edsm.net/api-v1/sphere-systems?showCoordinates=1&radius={SearchRadius}&showPermit=1&showInformation=1&showPrimaryStar=1";

        //    //var response = await client.GetStringAsync(url);

        //    var data = await webService.GetAllAsync();

        //    await SaveDataAsync(data);

        //    return data;
        //}

        //async Task SaveDataAsync(List<StarSystem> data)
        //{
        //    await localService.ClearDbAsync();
        //    //await App.Database.ClearDbAsync();

        //    foreach (var system in data)
        //    {
        //        await localService.SaveItemAsync(system);
        //        //await App.Database.SaveItemAsync(dbItem);
        //    }

        //    Preferences.Set("dbExpirationDate", DateTime.Now.AddDays(7).ToString());
        //    ////DEBUG
        //    //Preferences.Set("dbExpirationDate", DateTime.Now.AddSeconds(7).ToString());
        //}
    }
}
