using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChallengeMk2.Services;
using ChallengeMk2.Views;
using ChallengeMk2.DataBase;

namespace ChallengeMk2
{
    public partial class App : Application
    {
        static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                }

                return database;
            }
        }

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
