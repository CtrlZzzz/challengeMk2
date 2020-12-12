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
            SendRoomMessageCommand = new Command(async () => await SendRoomMessageAsync());
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

        public CollectionView CollectionToScroll { get; set; }

        public Command SendRoomMessageCommand { get; set; }



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
            chatService.Connection.On<MessageSentForm>("receiveNewRoomMessage", message =>
            {
                //TODO => check if incoming message is in the current room. if not, do not display it!
                RoomMessages.Add(message);
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
    }
}
