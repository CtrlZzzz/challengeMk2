using System;
using System.Collections.Generic;
using ChallengeMk2.DataBase;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChallengeMk2.Views
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();

            App.Database = new SQLiteDataService();
            App.Database.Initialize();

            ////Debug
            //Preferences.Remove("dbExpirationDate");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.Current.MainPage = new AppShell();
        }
    }
}
