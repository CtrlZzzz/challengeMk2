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

            Title = "Systems around SOL";

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

            try
            {
                Title = GetCurrentTitle(true);

                var data = await systemService.GetStarSystemDataAsync();

                Systems.Clear();
                foreach (var system in data)
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
                Title = GetCurrentTitle();

                IsBusy = false;
            }
        }

        string GetCurrentTitle(bool isRetreiving = false)
        {
            string title;

            if (isRetreiving)
            {
                if (systemService.GetLocalState())
                {
                    title = "Retreiving local data...";
                }
                else
                {
                    title = "Retreiving data from web API...";
                }
            }
            else
            {
                if (systemService.GetLocalState())
                {
                    title = "Systems around SOL (local)";
                }
                else
                {
                    title = "Systems around SOL (API)";
                }
            }

            return title;
        }
    }
}
