using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChallengeMk2.Services;
using ChallengeMk2.Views;

namespace ChallengeMk2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Debug
            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
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
