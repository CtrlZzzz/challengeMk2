using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using challengeMk2.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace challengeMk2.ViewModels
{
    public class StarSystemsViewModel : BaseViewModel
    {
        private StarSystem selectedSystem;
        public StarSystem SelectedSystem
        {
            set
            {
                SetProperty<StarSystem>(ref selectedSystem,value, onChanged: () => NavigateTodetailPage(selectedSystem));
            }
        }

        internal Action<StarSystem> NavigateTodetailPage { get; set; }  // Delelgate to call navigation when selecting an item in the list.

        public IList<StarSystem> Systems { get; set; }

        public Command LoadSystemDataCommand { get; set; }

        public NetworkAccess CurrentConnectivity { get; set; }



        public StarSystemsViewModel()
        {
            Title = "Star Systems around SOL";

            Systems = new ObservableCollection<StarSystem>();

            LoadSystemDataCommand = new Command(async () => await ExecuteLoadSystemDataCommand());                  
  
            ////Debug
            //Systems.Add(new StarSystem
            //{
            //    Name = "Sys 1",
            //    Distance = 45.78
            //});
            //Systems.Add(new StarSystem
            //{
            //    Name = "PoP 456",
            //    Distance = 23.7
            //});
            //Systems.Add(new StarSystem
            //{
            //    Name = "My Favorite System",
            //    Distance = 4.4
            //});
            //Systems.Add(new StarSystem
            //{
            //    Name = "LHS 333",
            //    Distance = 99.34
            //});
            //Systems.Add(new StarSystem
            //{
            //    Name = "HIP 434-563",
            //    Distance = 56.3
            //});
            //Systems.Add(new StarSystem
            //{
            //    Name = "Raxxla",
            //    Distance = 66.6
            //});
        }


        private async Task ExecuteLoadSystemDataCommand()   // Retreive Systems DATA from external API (EDSM)
        {
            CurrentConnectivity = Connectivity.NetworkAccess;

            string savedSystemsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EdsmOfflineData.json");


            if (CurrentConnectivity == NetworkAccess.Internet)  // User has acces to internet => retreive datas online from EDSM
            {
                IsBusy = true;  // For "PullToRefresh" systems list

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
            else  // User has no or bad connection => retreive datas from saved file
            {
                IsBusy = true;

                Title = "Offline Mode !";

                try
                {
                    //Display ALERT
                    await App.Current.MainPage.DisplayAlert("Connection issue", "Unable to connect to EDSM API. Switching to OFFLINE mode. If you have saved API datas, they will be loaded. If not, try to refresh later...", "OK");

                    //Get datas
                    var offlineData = GetOfflineData(savedSystemsFile);

                    //Store them
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

        private async Task<List<StarSystem>> GetDataFromApi(string fileToSaveDatas)
        {
            HttpClient client = new HttpClient();

            string url = "https://www.edsm.net/api-v1/sphere-systems?systemName=Sol&radius=30";

            var response = await client.GetStringAsync(url);

            //Save datas for offline mode
            File.WriteAllText(fileToSaveDatas, response);

            var datas = JsonConvert.DeserializeObject<List<StarSystem>>(response);

            return datas;
        }

        private List<StarSystem> GetOfflineData(string savedFile)
        {
            var offlineData = new List<StarSystem>();

            if (File.Exists(savedFile))  // User has already saved datas when he has internet connection
            {
                string offlineDatas = File.ReadAllText(savedFile);
                offlineData = JsonConvert.DeserializeObject<List<StarSystem>>(offlineDatas);
            }
            else  // User has no saved data :[
            {
                StarSystem noData = new StarSystem();
                noData.Name = "Sorry, no saved data found ! Try to refresh later.";
                offlineData.Add(noData);
            }

            return offlineData;
        }
    }
}
