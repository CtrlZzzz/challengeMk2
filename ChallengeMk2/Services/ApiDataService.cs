using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using Newtonsoft.Json;

namespace ChallengeMk2.Services
{
    class ApiDataService : IWebDataService
    {
        const int SearchRadius = 30;

        const string BaseUrl_All = "https://www.edsm.net/api-v1/sphere-systems";
        const string BaseUrl_Details = "https://www.edsm.net/api-v1/system";
        const string Details = "?&showInformation=1" +
                "&showId=1" +
                "&showPrimaryStar=1" +
                "&showPermit=1" +
                "&showCoordinates=1";

        public async Task<List<StarSystem>> GetAllAsync()
        {
            var url = BaseUrl_All + Details + $"&radius={SearchRadius}";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);

                //Should i move this convertion out of this and return the "brute" response
                //and convert it outside in another object ?
                var data = JsonConvert.DeserializeObject<List<StarSystem>>(response);

                return data;
            }
        }

        public async Task<StarSystem> GetDetails(string systemName)
        {
            var url = BaseUrl_Details + Details + $"&systemName={systemName}";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<StarSystem>(response);

                return data;
            }
        }

    }
}
