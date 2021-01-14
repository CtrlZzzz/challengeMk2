using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using ChallengeMk2.DebugTools;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace ChallengeMk2.Services
{
    class PuzzleService : IPuzzleService
    {

        public async Task<TryResult> GetTryResult(int userTry)
        {
            var currentResult = new TryResult();

            var apiRoute = GetApiRoute(userTry);

            HttpResponseMessage apiResponse;

            string responseContent;

#if DEBUG
            var insecureHandler = DependencyService.Get<IInsecureHandlerService>().GetInsecureHanler();
            using (var client = new HttpClient(insecureHandler))
            {
#else

            using (var client = new HttpClient())
            {

#endif

                apiResponse = await client.GetAsync(apiRoute);
                responseContent = await apiResponse.Content.ReadAsStringAsync();

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        currentResult = GetResult(HttpStatusCode.OK, responseContent, userTry);
                        break;

                    case HttpStatusCode.Accepted:
                        currentResult = GetResult(HttpStatusCode.Accepted, responseContent, userTry);
                        break;

                    case HttpStatusCode.ResetContent:
                        currentResult = new TryResult(20, apiResponse.ReasonPhrase, HttpStatusCode.ResetContent, userTry);
                        break;

                    case HttpStatusCode.InternalServerError:
                        currentResult = new TryResult(20, apiResponse.ReasonPhrase, HttpStatusCode.InternalServerError, userTry);
                        break;
                    default:
                        break;
                }

                return currentResult;
            }
        }


        string GetApiRoute(int userTry)
        {
            string apiBaseAddress;

            string apiRoute;

#if DEBUG
            //apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";    // Android Emulator on Windows 10
            apiBaseAddress = "https://thenumberfinderapi.azurewebsites.net";
#else

apiBaseAddress = "https://thenumberfinderapi.azurewebsites.net";

#endif

            return apiRoute = $"{apiBaseAddress}/api/TheNumber/{userTry}";
        }

        TryResult GetResult(HttpStatusCode status, string apiResponseContent, int userTry)
        {
            var apiResult = JsonConvert.DeserializeObject<TryResult>(apiResponseContent);

            return new TryResult(apiResult, status, userTry);
        }
    }
}
