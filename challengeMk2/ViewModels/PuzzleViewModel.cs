using System;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using ChallengeMk2.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ChallengeMk2.ViewModels
{
    public class PuzzleViewModel : BaseViewModel
    {
        static HttpClient client;



        public PuzzleViewModel()
        {
            Title = "Mi8 Puzzle challenge";

            client = new HttpClient();

            Tries = new ObservableCollection<TryResult>();

            CompareNumbersCommand = new Command(async () => await CompareNumbersAsync());
        }



        public Command CompareNumbersCommand { get; set; }

        public ObservableCollection<TryResult> Tries { get; set; }



        public async Task CompareNumbersAsync(int currentMin = 0, int currentMax = 50_000, int currentNumber = 25_000)
        {
            var userNumber = currentNumber;
            var min = currentMin;
            var max = currentMax;

            //string apiBaseAdress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";    // Android Emulator on Windows 10 ?
            var apiBaseAddress = Device.RuntimePlatform == Device.Android ? "https://localhost:5001" : "https://localhost:5001";
            var apiRoute = $"{apiBaseAddress}/api/TheNumber/{userNumber}";

            var apiResponse = await client.GetAsync(apiRoute);
            var responseContent = await apiResponse.Content.ReadAsStringAsync();

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    var currentResult = JsonConvert.DeserializeObject<TryResult>(responseContent);
                    if (currentResult.Result == "Smaller")
                    {
                        max = userNumber;
                        userNumber = GetMiddle(min, max);
                    }
                    else if (currentResult.Result == "Bigger")
                    {
                        min = userNumber;
                        userNumber = GetMiddle(min, max);
                    }

                    await CompareNumbersAsync(min, max, userNumber);

                    break;

                case HttpStatusCode.Accepted:
                    break;

                case HttpStatusCode.ResetContent:
                    break;

                case HttpStatusCode.InternalServerError:
                    break;
            }
        }

        int GetMiddle(int min, int max)
        {
            return min + ((max - min) / 2);
        }
    }
}
