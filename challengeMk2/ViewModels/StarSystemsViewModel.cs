using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChallengeMk2.ViewModels
{
    public class StarSystemsViewModel : BaseViewModel
    {
        public StarSystemsViewModel()
        {
            Title = "Star Systems around SOL";

            Systems = new ObservableCollection<StarSystem>();

            LoadSystemDataCommand = new Command(async () => await ExecuteLoadSystemDataCommand());
        }

        internal Action<StarSystem> NavigateTodetailPage { get; set; }  // Delelgate to call navigation when selecting an item in the list.

        StarSystem selectedSystem;
        public StarSystem SelectedSystem
        {
            set
            {
                if (value != null)
                {
                    SetProperty<StarSystem>(ref selectedSystem, value, onChanged: () => NavigateTodetailPage(selectedSystem));
                }
            }
        }

        public IList<StarSystem> Systems { get; set; }

        public Command LoadSystemDataCommand { get; set; }

        public NetworkAccess CurrentConnectivity { get; set; }

        async Task ExecuteLoadSystemDataCommand()   // Retreive Systems DATA from external API (EDSM)
        {
            CurrentConnectivity = Connectivity.NetworkAccess;

            var savedSystemsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EdsmOfflineData.json");


            if (CurrentConnectivity == NetworkAccess.Internet)
            {
                IsBusy = true;

                Title = "Star Systems around SOL";

                try
                {
                    //Get datas
                    var systemData = await GetDataFromApi(savedSystemsFile);

                    //Store them
                    Systems.Clear();

                    foreach (var system in systemData)
                    {
                        Systems.Add(system);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                IsBusy = true;

                Title = "Offline Mode !";

                try
                {
                    await App.Current.MainPage.DisplayAlert("Connection issue", "Unable to connect to EDSM API. Switching to OFFLINE mode. If you have saved API datas, they will be loaded. If not, try to refresh later...", "OK");

                    var offlineData = GetOfflineData(savedSystemsFile);

                    Systems.Clear();

                    foreach (var system in offlineData)
                    {
                        Systems.Add(system);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        async Task<List<StarSystem>> GetDataFromApi(string fileToSaveDatas)
        {
            using var client = new HttpClient();

            var url = "https://www.edsm.net/api-v1/sphere-systems?systemName=Sol&radius=30";

            var response = await client.GetStringAsync(url);

            File.WriteAllText(fileToSaveDatas, response);   //Save datas for offline mode

            var datas = JsonConvert.DeserializeObject<List<StarSystem>>(response);

            return datas;
        }

        List<StarSystem> GetOfflineData(string savedFile)
        {
            var offlineData = new List<StarSystem>();

            if (File.Exists(savedFile))  // User has already saved datas when he has internet connection
            {
                var offlineDatas = File.ReadAllText(savedFile);
                offlineData = JsonConvert.DeserializeObject<List<StarSystem>>(offlineDatas);
            }
            else  // User has no saved data :[
            {
                var noData = new StarSystem
                {
                    Name = "Sorry, no saved data found ! Try to refresh later."
                };
                offlineData.Add(noData);
            }

            return offlineData;
        }
    }
}
