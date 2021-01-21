using Xamarin.Forms;
using ChallengeMk2.MSAL;
using System.Linq;
using Microsoft.Identity.Client;
using Prism.Navigation;

namespace ChallengeMk2.Views
{
    public partial class ChatLoginMsalPage : ContentPage
    {
        public ChatLoginMsalPage()
        {
            InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    try
        //    {
        //        // Look for existing account
        //        var accounts = await App.AuthenticationClient.GetAccountsAsync();

        //        if (accounts.Count() >= 1)
        //        {
        //            var result = await App.AuthenticationClient
        //                .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
        //                .ExecuteAsync();

        //            //await Navigation.PushAsync();
        //            //await NavigationService.NavigateAsync(); 
        //        }
        //    }
        //    catch
        //    {
        //        // Do nothing - the user isn't logged in
        //    }

        //    base.OnAppearing();
        //}
    }
}
