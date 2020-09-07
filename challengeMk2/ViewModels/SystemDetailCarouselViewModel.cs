using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
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
            set
            {
                SetProperty<StarSystem>(ref currentSystem, value);
            }
        }

        StarSystem detailedSystem;
        public StarSystem DetailedSystem
        {
            get => detailedSystem;
            set
            {
                SetProperty<StarSystem>(ref detailedSystem, value);
            }
        }

        int currentBodyCount;
        public int CurrentBodyCount
        {
            get => currentBodyCount;
            set
            {
                SetProperty<int>(ref currentBodyCount, value);
            }
        }

        double currentDistance;
        public double CurrentDistance
        {
            get => currentDistance;
            set
            {
                SetProperty<double>(ref currentDistance, value);
            }
        }

        string currentBanner;
        public string CurrentBanner
        {
            get => currentBanner;
            set
            {
                SetProperty<string>(ref currentBanner, value);
            }
        }

        public Command SwitchBannerCommand { get; set; }

        public NetworkAccess CurrentConnectivity { get; set; }

        public ObservableCollection<StarSystem> SystemInfos { get; set; }







        void SetCurrentSystem(StarSystem selectedSystem)
        {
            currentSystem = selectedSystem;
        }


        async Task UpdateSystemData()
        {
            CurrentConnectivity = Connectivity.NetworkAccess;

            if (CurrentConnectivity == NetworkAccess.Internet)
            {
                DetailedSystem = await GetDetailsFromApi();
            }
            else
            {
                DetailedSystem.Name = "No internet connection ! Try again later...";
                return;
            }

            GetCompInfos();

            FillSystemInfos();
        }

        async Task<StarSystem> GetDetailsFromApi()
        {
            var encodedName = WebUtility.UrlEncode(currentSystem.Name);

            var url = $"https://www.edsm.net/api-v1/system?systemName={encodedName}&showInformation=1&showPrimaryStar=1&showPermit=1&showCoordinates=1";


            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<StarSystem>(response);
        }

        void GetCompInfos()
        {
            CurrentBodyCount = currentSystem.BodyCount;
            CurrentDistance = currentSystem.Distance;
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
                Name = detailedSystem.Name,
                RequirePermit = detailedSystem.RequirePermit,
                PermitName = detailedSystem.PermitName,
                Information = detailedSystem.Information,
                PrimaryStar = detailedSystem.PrimaryStar,
                Coords = detailedSystem.Coords
            };

            return systemWithId;
        }

        void SwitchBanner(int position)
        {
            CurrentBanner = banners[position];
        }

        public async Task InitializeAsync(StarSystem selectedSystem)
        {
            SetCurrentSystem(selectedSystem);

            await UpdateSystemData();
        }
    }
}
