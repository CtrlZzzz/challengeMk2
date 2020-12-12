using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Prism.Navigation;
using ChallengeMk2.Services;
using ChallengeMk2.Models.ChatModels;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChallengeMk2.ViewModels
{
    public class ChatLoginPageViewModel : PrismBaseViewModel
    {
        readonly IChatService chatService;

        public ChatLoginPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Discussions";
            LoginMessage = "";

            IsActive = true;

            chatService = chat;

            LoginCommand = new Command(async () => await TryToLogInAsync());
            NavigateToCreateAccountCommand = new Command(async () => await NavigateToCreateAccountAsync());
        }

        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        string entryName;
        public string EntryName
        {
            get => entryName; 
            set => SetProperty(ref entryName, value); 
        }

        string entryPassword;
        public string EntryPassword
        {
            get => entryPassword;
            set => SetProperty(ref entryPassword, value);
        }

        string loginMessage;
        public string LoginMessage
        {
            get => loginMessage; 
            set => SetProperty(ref loginMessage, value); 
        }

        public Command LoginCommand { get; set; }
        public Command NavigateToCreateAccountCommand { get; set; }
        


        async Task TryToLogInAsync()
        {
            try
            {
                IsBusy = true;
                IsActive = false;

                var loginResult = await chatService.LoginAsync(EntryName, EntryPassword);

                if (loginResult.IsSuccessful)
                {
                    await NavigationService.NavigateAsync("ChatMainPage");  
                }
                else
                {
                    LoginMessage = loginResult.Message;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsActive = true;
            }
        }

        async Task NavigateToCreateAccountAsync()
        {
            await NavigationService.NavigateAsync("ChatCreateAccountPage");
        }

        
    }
}
