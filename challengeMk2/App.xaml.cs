using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChallengeMk2.Services;
using ChallengeMk2.Views;
using ChallengeMk2.DataBase;
using System.Threading.Tasks;
using ChallengeMk2.Models;
using System.Diagnostics;

namespace ChallengeMk2
{
    public partial class App : Application
    {
        public static ILocalDataService Database { get; set; }

        public App()
        {
            InitializeComponent();

            //Debug
            DependencyService.Register<MockDataStore>();

            MainPage = new LoadingPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
