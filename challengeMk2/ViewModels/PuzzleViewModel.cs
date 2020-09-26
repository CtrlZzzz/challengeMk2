using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ChallengeMk2.Models;
using ChallengeMk2.DebugTools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChallengeMk2.ViewModels
{
    public class PuzzleViewModel : BaseViewModel
    {
        static HttpClient client;


        public PuzzleViewModel()
        {
            Title = "Mi8 Puzzle challenge";
        }


        bool canRunPuzzle = true;
        public bool CanRunPuzzle
        {
            get => canRunPuzzle;
            set
            {
                SetProperty<bool>(ref canRunPuzzle, value);
            }
        }

        string buttonText = "Run the Algorithm";
        public string ButtonText
        {
            get => buttonText;
            set
            {
                SetProperty<string>(ref buttonText, value);
            }
        }

        int? globalTry = null;
        public int? GlobalTry
        {
            get => globalTry;
            set
            {
                SetProperty<int?>(ref globalTry, value);
            }
        }

        int? bestTry = null;
        public int? BestTry
        {
            get
            {
                return bestTry;
            }
            set
            {
                SetProperty<int?>(ref bestTry, value);
            }
        }

        double? tryAverage = null;
        public double? TryAverage
        {
            get
            {
                return tryAverage;
            }
            set
            {
                SetProperty<double?>(ref tryAverage, value);
            }
        }

        List<int> winResultTries;
        public List<int> WinResultTries
        {
            get
            {
                return winResultTries;
            }
            set
            {
                SetProperty<List<int>>(ref winResultTries, value);
            }
        }

        public Command DisplayResultsCommand { get; set; }

        public ObservableCollection<TryResult> Tries { get; set; }


        public void InitializeViewModel()
        {
#if DEBUG
            var insecureHandler = DependencyService.Get<IInsecureHandlerService>().GetInsecureHanler();
            client = new HttpClient(insecureHandler);
#else

client = new HttpClient();

#endif

            Tries = new ObservableCollection<TryResult>();
            winResultTries = new List<int>();
            CreateStartupText();

            DisplayResultsCommand = new Command(async () => await DisplayResultsAsync());
        }

        public async Task DisplayResultsAsync()
        {
            CanRunPuzzle = false;
            ButtonText = "Running...";
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
                        currentResult = GetResult(HttpStatusCode.OK, responseContent, userTry);
                        CompareTry(currentResult, ref min, ref max, ref userTry);
                        break;

                    case HttpStatusCode.Accepted:
                        currentResult = GetResult(HttpStatusCode.Accepted, responseContent, userTry);
                        isPuzzleRunning = false;
                        CanRunPuzzle = true;
                        ButtonText = "Retry";
                        UpdateStatistics(currentResult.TryNumber);
                        break;

                    case HttpStatusCode.ResetContent:
                        currentResult = CreateResult(HttpStatusCode.ResetContent, 20, apiResponse.ReasonPhrase, userTry);
                        isPuzzleRunning = false;
                        CanRunPuzzle = true;
                        ButtonText = "You should modify something...";
                        break;

                    case HttpStatusCode.InternalServerError:
                        currentResult = CreateResult(HttpStatusCode.InternalServerError, 0, apiResponse.ReasonPhrase, userTry);
                        isPuzzleRunning = false;
                        break;
                }

                Tries.Insert(0, currentResult);

                await Task.Delay(1000);
            }
        }



        int GetMiddle(int min, int max)
        {
            return min + ((max - min) / 2);
        }

        TryResult GetResult(HttpStatusCode status, string apiResponseContent, int userTry)
        {
            var apiResult = JsonConvert.DeserializeObject<TryResult>(apiResponseContent);
            apiResult.UserTry = userTry;
            apiResult.Status = status;

            return apiResult;
        }

        TryResult CreateResult(HttpStatusCode status, int tryNumber, string result, int userTry)
        {
            var apiResult = new TryResult
            {
                TryNumber = tryNumber,
                Result = result,
                UserTry = userTry,
                Status = status
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

        void CreateStartupText()
        {
            var container = new TryResult
            {
                Result = "find a number between 1 and 50 000 in 20 tries.\n" +
                "Run the local API, call the api/TheNumber/ route \n" +
                "then display the results of all tries until win !",
                Status = HttpStatusCode.ResetContent
            };

            Tries.Clear();
            Tries.Add(container);
        }

        void UpdateStatistics(int tryNumber)
        {
            if (globalTry == null)
            {
                globalTry = 0;
            }
            GlobalTry++;

            WinResultTries.Add(tryNumber);

            TryAverage = Math.Round(winResultTries.Average(), 2);

            if (bestTry == null)
            {
                bestTry = 20;
            }
            if (tryNumber < bestTry)
            {
                BestTry = tryNumber;
            }
        }
    }
}
