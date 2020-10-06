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
            string apiBaseAddress;

            string apiRoute;
            HttpResponseMessage apiResponse;
            string responseContent;


#if DEBUG
            apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";    // Android Emulator on Windows 10
            //apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://localhost:5001" : "https://localhost:5001";  //ATM, impossible to connect android device to this address...

            var insecureHandler = DependencyService.Get<IInsecureHandlerService>().GetInsecureHanler();
            using var client = new HttpClient(insecureHandler);
#else

//var apiBaseAddress = "!!!Paste API Server address here!!!";
using var client = new HttpClient();

#endif


            apiRoute = $"{apiBaseAddress}/api/TheNumber/{userTry}";
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

        TryResult GetResult(HttpStatusCode status, string apiResponseContent, int userTry)
        {
            var apiResult = JsonConvert.DeserializeObject<TryResult>(apiResponseContent);
            //apiResult.UserTry = userTry;
            //apiResult.Status = status;

            //return apiResult;
            return new TryResult(apiResult, status, userTry);
        }

        //TryResult CreateResult(string result, HttpStatusCode status, int userTry)
        //{
        //    //var apiResult = new TryResult(20, result, status, userTry);
        //    //{
        //    //    TryNumber = 20,
        //    //    Result = result,
        //    //    UserTry = userTry,
        //    //    Status = status
        //    //};

        //    //return apiResult;
        //    return new TryResult(20, result, status, userTry);
        //}
    }
}
