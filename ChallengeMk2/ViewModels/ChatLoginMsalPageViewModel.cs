using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.AppModel;
using Prism.Navigation;
using ChallengeMk2.MSAL;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChallengeMk2.ViewModels
{
    public class ChatLoginMsalPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IInitializeAsync
    {
        IAuthenticationService authenticationService;

        public ChatLoginMsalPageViewModel(INavigationService navigationService, IAuthenticationService authService) : base(navigationService)
        {
            Title = "Discussions";
            authenticationService = authService;
        }

        public Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public async void OnAppearing() => await LoginAsync();
        public void OnDisappearing()
        {
        }

        async Task LoginAsync()
        {
            try
            {
                IsBusy = true;

                await authenticationService.AutoSignIn();

                //await NavigationService.NavigateAsync("");
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
}
