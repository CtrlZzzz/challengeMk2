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
    public class ChatCreateAccountPageViewModel : PrismBaseViewModel
    {
        readonly IChatService chatService;

        public ChatCreateAccountPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "New account";
            AlertMessage = "";

            IsActive = true;

            chatService = chat;

            CreateAccountCommand = new Command(async () => await CreateAccountAsync());
        }

        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value); 
        }

        string entryUsername;
        public string EntryUsername
        {
            get => entryUsername;
            set => SetProperty(ref entryUsername, value);
        }

        string entryPassword;
        public string EntryPassword
        {
            get => entryPassword;
            set => SetProperty(ref entryPassword, value);
        }

        string entryFirstName;
        public string EntryFirstName
        {
            get => entryFirstName;
            set => SetProperty(ref entryFirstName, value);
        }

        string entryLastName;
        public string EntryLastName
        {
            get => entryLastName;
            set => SetProperty(ref entryLastName, value);
        }

        string alertMessage;
        public string AlertMessage
        {
            get => alertMessage;
            set => SetProperty(ref alertMessage, value);
        }

        public Command CreateAccountCommand { get; set; }


        async Task CreateAccountAsync()
        {
            try
            {
                IsBusy = true;
                IsActive = false;

                var creationResult = await chatService.CreateAccountAsync(EntryUsername, EntryPassword, EntryFirstName, EntryLastName);

                if (creationResult.IsSuccessful)
                {
                    await NavigationService.NavigateAsync("ChatMainPage");
                }
                else
                {
                    AlertMessage = creationResult.Message;
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
    }
}
