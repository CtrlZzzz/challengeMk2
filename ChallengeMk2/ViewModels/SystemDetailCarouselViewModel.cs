using System.Collections.ObjectModel;
using ChallengeMk2.Models;
using Xamarin.Forms;


namespace ChallengeMk2.ViewModels
{
    public class SystemDetailCarouselViewModel : BaseViewModel
    {
        readonly string[] banners;


        public SystemDetailCarouselViewModel()
        {
            Title = "Star System Details";

            SystemInfos = new ObservableCollection<StarSystem>();

            banners = new string[]
            {
                "BannerDetail_01",
                "BannerDetail_08",
                "BannerDetail_04",
                "BannerDetail_07"
            };

            SwitchBannerCommand = new Command<int>(p => SwitchBanner(p));
            currentBanner = banners[0];
        }


        StarSystem currentSystem;
        public StarSystem CurrentSystem
        {
            get => currentSystem;
            set => SetProperty(ref currentSystem, value);
        }

        string currentBanner;
        public string CurrentBanner
        {
            get => currentBanner;
            set => SetProperty(ref currentBanner, value);
        }

        public Command SwitchBannerCommand { get; set; }

        public ObservableCollection<StarSystem> SystemInfos { get; set; }


        void SetCurrentSystem(StarSystem selectedSystem)
        {
            CurrentSystem = selectedSystem;
        }

        void FillSystemInfos()
        {
            SystemInfos.Add(GetSystemWithIdSelector(0));
            SystemInfos.Add(GetSystemWithIdSelector(1));
            SystemInfos.Add(GetSystemWithIdSelector(2));
            SystemInfos.Add(GetSystemWithIdSelector(3));
        }

        StarSystem GetSystemWithIdSelector(int id)
        {
            var systemWithId = new StarSystem
            {
                DataSelectorID = id,

                Distance = currentSystem.Distance,
                BodyCount = currentSystem.BodyCount,
                Name = currentSystem.Name,
                RequirePermit = currentSystem.RequirePermit,
                PermitName = currentSystem.PermitName,
                Information = currentSystem.Information,
                PrimaryStar = currentSystem.PrimaryStar,
                Coords = currentSystem.Coords
            };

            return systemWithId;
        }

        void SwitchBanner(int position)
        {
            CurrentBanner = banners[position];
        }

        public void InitializeViewModel(StarSystem selectedSystem)
        {
            SetCurrentSystem(selectedSystem);

            FillSystemInfos();
        }
    }
}
