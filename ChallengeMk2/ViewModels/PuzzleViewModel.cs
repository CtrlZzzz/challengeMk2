using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using ChallengeMk2.Services;
using Prism.Navigation;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    public class PuzzleViewModel : PrismBaseViewModel
    {
        readonly IPuzzleService puzzle;

        public PuzzleViewModel(INavigationService navigationService, IPuzzleService puzzleService) : base(navigationService)
        {
            Title = "Number finder";

            puzzle = puzzleService;
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
            TryResult currentResult;

            var isPuzzleRunning = true;
            while (isPuzzleRunning)
            {
                currentResult = await puzzle.GetTryResult(userTry);

                var status = currentResult.Status;

                switch (status)
                {
                    case HttpStatusCode.OK:
                        CompareTry(currentResult, ref min, ref max, ref userTry);
                        break;

                    case HttpStatusCode.Accepted:
                        isPuzzleRunning = false;
                        CanRunPuzzle = true;
                        ButtonText = "Retry";
                        UpdateStatistics(currentResult.TryNumber);
                        break;

                    case HttpStatusCode.ResetContent:
                        isPuzzleRunning = false;
                        CanRunPuzzle = true;
                        ButtonText = "You should modify something...";
                        break;

                    case HttpStatusCode.InternalServerError:
                        isPuzzleRunning = false;
                        break;
                }

                Tries.Insert(0, currentResult);

                await Task.Delay(600);
            }
        }



        int GetMiddle(int min, int max)
        {
            return min + ((max - min) / 2);
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
                Result = "Find a number between 1 and 50 000 in 20 tries.\n" +
                "\n" +
                "Call the https://thenumberfinderapi.azurewebsites.net/api/TheNumber/. \n" +
                "\n" +
                "Display the results of all your tries until win !",
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
