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
using ChallengeMk2.MSAL;
using ChallengeMk2.Models.ChatModels;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Prism;

namespace ChallengeMk2.ViewModels
{
    public class ChatMainPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize//, IActiveAware
    {
        readonly IChatService chatService;

        readonly IAuthenticationService authenticationService;

        public ChatMainPageViewModel(INavigationService navigationService, IChatService chat, IAuthenticationService authService) : base(navigationService)
        {
            Title = "Discussions";
            chatService = chat;
            authenticationService = authService;
            Rooms = new ObservableCollection<RoomListObject>();
            Contacts = new ObservableCollection<Contact>();

            NavigateToPublicChatCommand = new Command(async () => await NavigateToPublicChatAsync());
            NavigateToAddRoomCommand = new Command(async () => await NavigateToAddRoomAsync());
            NavigateToRoomCommand = new Command<string>(async (id) => await NavigateToRoomAsync(id));
            NavigateToAddContactCommand = new Command(async () => await NavigateToAddContactAsync());
            NavigateToPrivateCommand = new Command<Contact>(async (contact) => await NavigateToPrivateAsync(contact));
            SignOutCommand = new Command(async () => await SignOutAsync());
        }

        //public event EventHandler IsActiveChanged;

        //bool isActive;
        //public bool IsActive
        //{
        //    get { return isActive; }
        //    set { SetProperty(ref isActive, value, () => IsActiveChanged?.Invoke(this, EventArgs.Empty)); }
        //}

        User connectedUser;
        public User ConnectedUser
        {
            get { return connectedUser; }
            set { SetProperty(ref connectedUser, value); }
        }

        ObservableCollection<RoomListObject> rooms;
        public ObservableCollection<RoomListObject> Rooms
        {
            get { return rooms; }
            set { SetProperty(ref rooms, value); }
        }

        ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set { SetProperty(ref contacts, value); }
        }

        public Command NavigateToPublicChatCommand { get; set; }
        public Command NavigateToAddRoomCommand { get; set; }
        public Command<string> NavigateToRoomCommand { get; set; }
        public Command NavigateToAddContactCommand { get; set; }
        public Command<Contact> NavigateToPrivateCommand { get; set; }
        public Command SignOutCommand { get; set; }

        //void OnIsActiveChanged(object sender, EventArgs e)
        //{
        //    if (IsActive)
        //    {
        //        authenticationService.AutoSignIn();
        //    }
        //}

        public void OnAppearing()
        {
            InitializeConnection();
            InitializeViewModel();

            //IsActiveChanged += OnIsActiveChanged;
        }

        public void OnDisappearing()
        {
            //IsActiveChanged -= OnIsActiveChanged;
        }

        void InitializeConnection()
        {
            chatService.Connection.Closed += async (error) =>
            {
                chatService.IsConnected = false;
                //TODO Warn user that he is disconnected => "Connection closed - Try to reconnect in few seconds..."
                var random = new Random();
                await Task.Delay(random.Next(0, 5) * 1000);
                await chatService.ConnectAsync();
            };

            chatService.Connection.On<Contact>("receiveNewContact", contact =>
            {
                Contacts.Add(contact);
            });
        }

        void InitializeViewModel()
        {
            var userRooms = chatService.ConnectedUser.Rooms;

            //TODO => move this in a separate mapper service?
            var mappedData = new List<RoomListObject>();
            foreach (var item in userRooms)
            {
                mappedData.Add(new RoomListObject(item.RoomId, item.RoomName));
            }
            Rooms = new ObservableCollection<RoomListObject>(mappedData);

            var userContacts = chatService.ConnectedUser.Contacts;
            Contacts = new ObservableCollection<Contact>(userContacts);

            ConnectedUser = chatService.ConnectedUser;
        }

        async Task NavigateToPublicChatAsync()
        {
            var existingMessages = new ObservableCollection<MessageSentForm>(await chatService.GetAllPublicMessagesAsync());
            await NavigationService.NavigateAsync("ChatPublicPage", ("PublicMessages", existingMessages));
        }

        async Task NavigateToAddRoomAsync()
        {
            var existingRooms = new ObservableCollection<RoomListObject>(await chatService.GetAllRoomsAsync());
            await NavigationService.NavigateAsync("ChatAddRoomPage", ("ExistingRooms", existingRooms));
        }

        async Task NavigateToRoomAsync(string roomId)
        {
            var room = await chatService.GetRoomAsync(roomId);
            await NavigationService.NavigateAsync("ChatRoomPage", ("CurrentRoom", room));
        }

        async Task NavigateToAddContactAsync()
        {
            var existingUsers = new ObservableCollection<UserListObject>(await chatService.GetAllUsersAsync());
            await NavigationService.NavigateAsync("ChatAddContactPage", ("ExistingUsers", existingUsers));
        }

        async Task NavigateToPrivateAsync(Contact privateContact)
        {
            await NavigationService.NavigateAsync("ChatPrivatePage", ("CurrentPrivate", privateContact));
        }

        async Task SignOutAsync()
        {
            await chatService.DisconnectAsync();
            await NavigationService.NavigateAsync("ChatLoginPage");
        }

    }
}
