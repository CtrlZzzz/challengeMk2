using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ChallengeMk2.Models;
using ChallengeMk2.DebugTools;
using Newtonsoft.Json;

namespace ChallengeMk2.ViewModels
{
    public class PuzzleViewModel : BaseViewModel
    {
        static HttpClient client;



        public PuzzleViewModel()
        {
            Title = "Mi8 Puzzle challenge";

#if DEBUG
            var insecureHandler = DependencyService.Get<IInsecureHandlerService>().GetInsecureHanler();
            client = new HttpClient(insecureHandler);
#else

client = new HttpClient();

#endif

            Tries = new ObservableCollection<TryResult>();

            DisplayResultsCommand = new Command(async () => await DisplayResultsAsync());
        }



        public Command DisplayResultsCommand { get; set; }

        public ObservableCollection<TryResult> Tries { get; set; }



        public async Task DisplayResultsAsync()
        {
            Tries.Clear();

            var userTry = 25_000;
            var min = 0;
            var max = 50_000;
            var currentResult = new TryResult();

            //var apiBaseAddress = "!!!Paste API Server address here!!!";

            //Debug
            //var apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://localhost:5001" : "https://localhost:5001";  //ATM, impossible to connect android device to this address...
            var apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";    // Android Emulator on Windows 10

            string apiRoute;
            HttpResponseMessage apiResponse;
            string responseContent;

            var isPuzzleRunning = true;
            while (isPuzzleRunning)
            {
                apiRoute = $"{apiBaseAddress}/api/TheNumber/{userTry}";
                apiResponse = await client.GetAsync(apiRoute);
                responseContent = await apiResponse.Content.ReadAsStringAsync();

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        currentResult = GetResult(responseContent, userTry);
                        CompareTry(currentResult, ref min, ref max, ref userTry);
                        break;

                    case HttpStatusCode.Accepted:
                        currentResult = GetResult(responseContent, userTry);
                        isPuzzleRunning = false;
                        break;

                    case HttpStatusCode.ResetContent:
                        currentResult = CreateResult(20, apiResponse.ReasonPhrase, userTry);
                        isPuzzleRunning = false;
                        break;

                    case HttpStatusCode.InternalServerError:
                        currentResult = CreateResult(0, apiResponse.ReasonPhrase, userTry);
                        isPuzzleRunning = false;
                        break;
                }

                Tries.Add(currentResult);

                await Task.Delay(1000);
            }
        }

        int GetMiddle(int min, int max)
        {
            return min + ((max - min) / 2);
        }

        TryResult GetResult(string apiResponseContent, int userTry)
        {
            var apiResult = JsonConvert.DeserializeObject<TryResult>(apiResponseContent);
            apiResult.UserTry = userTry;

            return apiResult;
        }

        TryResult CreateResult(int tryNumber, string result, int userTry)
        {
            var apiResult = new TryResult
            {
                TryNumber = tryNumber,
                Result = result,
                UserTry = userTry
            };

            return apiResult;
        }

        void CompareTry(TryResult result, ref int currentMin, ref int currentMax, ref int currentTry)
        {
            if (result.Result == "Smaller")
            {
                currentMax = currentTry;
                currentTry = GetMiddle(currentMin, currentMax);
            }
            else if (result.Result == "Bigger")
            {
                currentMin = currentTry;
                currentTry = GetMiddle(currentMin, currentMax);
            }
        }
    }
}
