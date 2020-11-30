using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using ChallengeMk2.Services;
using Xamarin.Forms;
using Prism.Navigation;

namespace ChallengeMk2.ViewModels
{
    public class StarSystemsViewModel : PrismBaseViewModel
    {
        IStarSystemService systemService;

        public StarSystemsViewModel(INavigationService navigationService, IStarSystemService starSystemService) : base(navigationService)
        {
            
            InitializeViewModel(starSystemService);
        }

        StarSystem selectedSystem;
        public StarSystem SelectedSystem
        {
            set
            {
                SetProperty(ref selectedSystem, value);/*, onChanged: () =>
                {
                    NavigateTodetailPage(selectedSystem);
                    if (value != null)
                    {
                        selectedSystem = null;
                    }
                });*/
            }
        }

        IList<StarSystem> systems;
        public IList<StarSystem> Systems
        {
            get => systems;
            set => SetProperty(ref systems, value);
        }

        public Command DisplaySystemDataCommand { get; set; }

        public Command<StarSystem> NavigateToDetailCommand { get; set; }

        //internal Action<StarSystem> NavigateTodetailPage { get; set; }


        void InitializeViewModel(IStarSystemService starSystemService)
        {
            systemService = starSystemService;

            Title = "System finder";

            Systems = new ObservableCollection<StarSystem>();

            DisplaySystemDataCommand = new Command(async () => await DisplaySystemDataAsync());

            NavigateToDetailCommand = new Command<StarSystem>(async (selectedSystem) => await NavigationService.NavigateAsync("SystemDetailCarouselPage", ("CurrentSystem", selectedSystem)));
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
                //title = systemService.GetLocalState() ? "Systems around SOL (local)" : "Systems around SOL (API)";
                title = "System finder";
            }

            return title;
        }
    }
}
