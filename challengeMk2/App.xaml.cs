using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using challengeMk2.Services;
using challengeMk2.Views;

namespace challengeMk2
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
