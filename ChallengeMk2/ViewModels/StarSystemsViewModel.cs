using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using ChallengeMk2.Services;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    public class StarSystemsViewModel : BaseViewModel
    {
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
                SetProperty(ref selectedSystem, value, onChanged: () =>
                {
                    NavigateTodetailPage(selectedSystem);
                    if (value != null)
                    {
                        selectedSystem = null;
                    }
                });
            }
        }

        IList<StarSystem> systems;
        public IList<StarSystem> Systems
        {
            get => systems;
            set => SetProperty(ref systems, value);
        }

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
            systemService = DependencyService.Get<IStarSystemService>();
        }

        async Task DisplaySystemDataAsync()
        {
            IsBusy = true;

            try
            {
                Title = GetCurrentTitle(true);

                var data = await systemService.GetStarSystemDataAsync();

                Systems = new ObservableCollection<StarSystem>(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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
                title = systemService.GetLocalState() ? "Retreiving local data..." : "Retrieving data from web API...";
            }
            else
            {
                title = systemService.GetLocalState() ? "Systems around SOL (local)" : "Systems around SOL (API)";
            }

            return title;
        }
    }
}
