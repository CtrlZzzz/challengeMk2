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
    public class ChatRoomPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize
    {
        readonly IChatService chatService;

        public ChatRoomPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Discussions";
            chatService = chat;
            RoomMessages = new ObservableCollection<MessageSentForm>();
            RoomUsers = new ObservableCollection<UserInRoomObject>();
            SendRoomMessageCommand = new Command(async () => await SendRoomMessageAsync());
            QuitRoomCommand = new Command(async () => await QuitRoomAsync());
            NavigateToMainPageCommand = new Command(async () => await NavigateToMainPageAsync());
        }

        Room currentRoom;
        public Room CurrentRoom
        {
            get => currentRoom;
            set => SetProperty(ref currentRoom, value);
        }

        string entryRoomMessage;
        public string EntryRoomMessage
        {
            get => entryRoomMessage;
            set => SetProperty(ref entryRoomMessage, value);
        }

        ObservableCollection<MessageSentForm> roomMessages;
        public ObservableCollection<MessageSentForm> RoomMessages
        {
            get { return roomMessages; }
            set { SetProperty(ref roomMessages, value); }
        }

        ObservableCollection<UserInRoomObject> roomUsers;
        public ObservableCollection<UserInRoomObject> RoomUsers
        {
            get { return roomUsers; }
            set { SetProperty(ref roomUsers, value); }
        }

        public CollectionView CollectionToScroll { get; set; }

        public Command SendRoomMessageCommand { get; set; }
        public Command QuitRoomCommand { get; set; }
        public Command NavigateToMainPageCommand { get; set; }



        public void OnAppearing()
        {
            InitializeConnection();
            InitializeViewModel();
        }
        public void OnDisappearing()
        {
        }

        void InitializeConnection()
        {
            chatService.Connection.On<MessageSentForm, string>("receiveNewRoomMessage", (message, roomId) =>
            {
                if (roomId == currentRoom.Id)
                {
                    RoomMessages.Add(message);
                }
            });

            chatService.Connection.On<string, string>("newJoinRoom", async (connectionId, userId) =>
            {
                var user = await chatService.GetUserAsync(userId);
                var convertedUser = new UserInRoomObject(user.Id, user.Username);
                roomUsers.Add(convertedUser);
            });

            chatService.Connection.On<string, string>("newQuitRoom", (connectionId, userId) =>
            {
                var userToRemove = RoomUsers.SingleOrDefault(u => u.UserId == userId);
                if (userToRemove != null)
                {
                    RoomUsers.Remove(userToRemove);
                }
            });
        }

        void InitializeViewModel()
        {
            Title = CurrentRoom.RoomName;

            //TODO => move this in a separate mapper service?
            var mappedData = new List<MessageSentForm>();
            foreach (var item in CurrentRoom.RoomMessages)
            {
                mappedData.Add(new MessageSentForm(item.SenderId, item.SenderName, item.Content));
            }

            RoomMessages = new ObservableCollection<MessageSentForm>(mappedData);

            RoomUsers = new ObservableCollection<UserInRoomObject>(currentRoom.RoomUsers);

            if (roomMessages.Count != 0)
            {
                CollectionToScroll.ScrollTo(roomMessages.Count - 1);
            }
        }

        async Task SendRoomMessageAsync()
        {
            await chatService.SendRoomMessageAsync(EntryRoomMessage, currentRoom.Id);
            EntryRoomMessage = "";
        }

        async Task QuitRoomAsync()
        {
            try
            {
                IsBusy = true;

                await chatService.QuitRoomAsync(currentRoom.Id);
                await NavigationService.GoBackAsync();
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

        async Task NavigateToMainPageAsync()
        {
            await NavigationService.NavigateAsync("ChatMainPage");
        }
    }
}
