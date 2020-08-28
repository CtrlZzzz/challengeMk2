using System;
using System.Net;
using ChallengeMk2.Models;
using ChallengeMk2.ViewModels;
using System.IO;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ChallengeMk2.ViewModels
{
    public class SystemDetailViewModel : BaseViewModel
    {
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



        public NetworkAccess CurrentConnectivity { get; set; }


        public SystemDetailViewModel(StarSystem selectedSystem = null)
        {
            Title = "Star System Details";
            //Title = selectedSystem?.Name;

            CurrentSystem = selectedSystem;

          
            UpdateSystemData();
        }





         async void UpdateSystemData()
        {
            CurrentConnectivity = Connectivity.NetworkAccess;

            //1° Get details from online API
            if (CurrentConnectivity == NetworkAccess.Internet)
            {
                DetailedSystem = await GetDetailsFromApi();
            }
            else
            {
                DetailedSystem.Name = "No internet connection ! Try again later...";
                return;
            }

            //2° Get distance and number of bodies from currentSystem and put them in detailedSystem
            GetCompInfos();

        }

         async Task<StarSystem> GetDetailsFromApi()
        {
            HttpClient client = new HttpClient();

            // Check system name for special characters : "+" must be replace by "%2b" => Try WebUtility.HtmlEncode(string)
            string encodedName = WebUtility.UrlEncode(currentSystem.Name);

            string url = $"https://www.edsm.net/api-v1/system?systemName={encodedName}&showInformation=1&showPrimaryStar=1&showPermit=1&showCoordinates=1";

            var response = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<StarSystem>(response);
        }

         void GetCompInfos()
        {
            CurrentBodyCount = currentSystem.BodyCount;
            CurrentDistance = currentSystem.Distance;
        }
    }
}
