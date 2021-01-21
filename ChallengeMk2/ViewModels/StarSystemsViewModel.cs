using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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


        string entrySearchMessage;
        public string EntrySearchMessage
        {
            get => entrySearchMessage;
            set => SetProperty(ref entrySearchMessage, value);
        }

        int selectionId;
        public int SelectionId
        {
            get => selectionId;
            set => SetProperty(ref selectionId, value);
        }

        ObservableCollection<StarSystem> systems;
        public ObservableCollection<StarSystem> Systems
        {
            get => systems;
            set => SetProperty(ref systems, value);
        }

        ObservableCollection<StarSystem> sortedSystems;
        public ObservableCollection<StarSystem> SortedSystems
        {
            get => sortedSystems;
            set => SetProperty(ref sortedSystems, value);
        }

        public Command DisplaySystemDataCommand { get; set; }
        public Command<StarSystem> NavigateToDetailCommand { get; set; }
        public Command SearchCommand { get; set; }


        void InitializeViewModel(IStarSystemService starSystemService)
        {
            systemService = starSystemService;

            Title = "System finder";

            Systems = new ObservableCollection<StarSystem>();
            SortedSystems = new ObservableCollection<StarSystem>();

            DisplaySystemDataCommand = new Command(async () => await DisplaySystemDataAsync());
            NavigateToDetailCommand = new Command<StarSystem>(async (selectedSystem) =>
            {
                SelectionId = selectedSystem.Id;
                await NavigationService.NavigateAsync("SystemDetailCarouselPage", ("CurrentSystem", selectedSystem));
            });
            SearchCommand = new Command(() => Search());
        }


        async Task DisplaySystemDataAsync()
        {
            IsBusy = true;

            try
            {
                Title = GetCurrentTitle(true);

                var data = await systemService.GetStarSystemDataAsync();

                Systems = new ObservableCollection<StarSystem>(data);
                SortedSystems = new ObservableCollection<StarSystem>(systems);
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
                title = "System finder";
            }

            return title;
        }

        void Search()
        {
            if (string.IsNullOrWhiteSpace(EntrySearchMessage))
            {
                SortedSystems = new ObservableCollection<StarSystem>(Systems);
            }
            else
            {
                var searchedSystems = Systems.Where(s => s.Name.ToLower().StartsWith(EntrySearchMessage.ToLower()));
                SortedSystems = new ObservableCollection<StarSystem>(searchedSystems);
            }
        }
    }
}
