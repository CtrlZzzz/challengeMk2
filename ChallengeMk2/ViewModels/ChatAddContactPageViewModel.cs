using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.AppModel;
using Prism.Navigation;
using ChallengeMk2.Services;
using ChallengeMk2.Models.ChatModels;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace ChallengeMk2.ViewModels
{
    public class ChatAddContactPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize
    {
        readonly IChatService chatService;

        public ChatAddContactPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Add new contact";
            IsActive = true;
            chatService = chat;
            ExistingUsers = new ObservableCollection<UserListObject>();
            SortedUsers = new ObservableCollection<UserListObject>();
            SearchCommand = new Command(() => Search());
            AddContactCommand = new Command<UserListObject>(async (u) => await AddContactAsync(u));
        }


        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        string entrySearchMessage;
        public string EntrySearchMessage
        {
            get => entrySearchMessage;
            set => SetProperty(ref entrySearchMessage, value);
        }

        ObservableCollection<UserListObject> existingUsers;
        public ObservableCollection<UserListObject> ExistingUsers
        {
            get { return existingUsers; }
            set { SetProperty(ref existingUsers, value); }
        }

        ObservableCollection<UserListObject> sortedUsers;
        public ObservableCollection<UserListObject> SortedUsers
        {
            get { return sortedUsers; }
            set { SetProperty(ref sortedUsers, value); }
        }

        public Command SearchCommand { get; set; }
        public Command<UserListObject> AddContactCommand { get; set; }

        public void OnAppearing() => InitializeViewModel();
        
        public void OnDisappearing()
        {
        }

        void InitializeViewModel()
        {
            var copy = new List<UserListObject>(existingUsers);
            for (var i = 0; i < copy.Count; i++)
            {
                if (copy[i].Id == chatService.ConnectedUser.Id)
                {
                    ExistingUsers.Remove(copy[i]);
                }
            }

            var userContacts = chatService.ConnectedUser.Contacts;
            if (userContacts.Count == 0)
            {
                return;
            }

            copy = new List<UserListObject>(existingUsers);
            foreach (var item in userContacts)
            {
                for (var i = 0; i < copy.Count; i++)
                {
                    if (copy[i].Id == item.ContactId)
                    {
                        ExistingUsers.Remove(copy[i]);
                        i = copy.Count;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        void Search()
        {
            if (string.IsNullOrWhiteSpace(EntrySearchMessage))
            {
                SortedUsers = new ObservableCollection<UserListObject>();
            }
            else
            {
                var searchedUsers = existingUsers.Where(u => u.Username.ToLower().StartsWith(EntrySearchMessage.ToLower()));
                SortedUsers = new ObservableCollection<UserListObject>(searchedUsers);
            }
        }

        async Task AddContactAsync(UserListObject newContact)
        {
            try
            {
                IsBusy = true;
                IsActive = false;

                await chatService.AddContactAsync(newContact.Id, newContact.Username);

                ExistingUsers.Remove(newContact);

                var searchedUsers = existingUsers.Where(u => u.Username.ToLower().StartsWith(EntrySearchMessage.ToLower()));
                SortedUsers = new ObservableCollection<UserListObject>(searchedUsers);
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
