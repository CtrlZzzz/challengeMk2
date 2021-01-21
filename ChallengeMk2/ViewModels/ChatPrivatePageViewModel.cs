using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Navigation;
using ChallengeMk2.Services;
using ChallengeMk2.Models.ChatModels;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChallengeMk2.ViewModels
{
    public class ChatPrivatePageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize
    {
        readonly IChatService chatService;

        public ChatPrivatePageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Discussions";
            chatService = chat;
            PrivateMessages = new ObservableCollection<MessageSentForm>();
            SendPrivateMessageCommand = new Command(async () => await SendPrivateMessageAsync());
            GoBackAndUpdateUserCommand = new Command(async () => await GoBackAndUpdateUserAsync());
        }

        Contact currentPrivate;
        public Contact CurrentPrivate
        {
            get => currentPrivate;
            set => SetProperty(ref currentPrivate, value);
        }

        string entryPrivateMessage;
        public string EntryPrivateMessage
        {
            get => entryPrivateMessage;
            set => SetProperty(ref entryPrivateMessage, value);
        }

        ObservableCollection<MessageSentForm> privateMessages;
        public ObservableCollection<MessageSentForm> PrivateMessages
        {
            get { return privateMessages; }
            set { SetProperty(ref privateMessages, value); }
        }

        public CollectionView CollectionToScroll { get; set; }

        public Command SendPrivateMessageCommand { get; set; }

        public Command GoBackAndUpdateUserCommand { get; set; }

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
            chatService.Connection.On<Message, string>("receiveNewPrivateMessage", (message, userId) =>
            {
                if (currentPrivate.ContactId == userId || chatService.ConnectedUser.Id == userId)
                {
                    var mappedData = new MessageSentForm(message.SenderId, message.SenderName, message.Content);
                    PrivateMessages.Add(mappedData);
                }
            });

            chatService.Connection.On<Message>("addedPrivateMessageOffline", message =>
            {
                var mappedData = new MessageSentForm(message.SenderId, message.SenderName, message.Content);
                PrivateMessages.Add(mappedData);
            });
        }

        void InitializeViewModel()
        {
            Title = CurrentPrivate.ContactName;

            //TODO => move this in a separate mapper service?
            var mappedData = new List<MessageSentForm>();
            foreach (var item in CurrentPrivate.Messages)
            {
                mappedData.Add(new MessageSentForm(item.SenderId, item.SenderName, item.Content));
            }

            PrivateMessages = new ObservableCollection<MessageSentForm>(mappedData);

            if (PrivateMessages.Count != 0)
            {
                CollectionToScroll.ScrollTo(PrivateMessages.Count - 1);
            }
        }

        async Task SendPrivateMessageAsync()
        {
            await chatService.SendPrivateMessageAsync(EntryPrivateMessage, currentPrivate.ContactId);
            EntryPrivateMessage = "";
        }

        async Task GoBackAndUpdateUserAsync()
        {
            await chatService.UpdateUserInfoAsync();
            await NavigationService.GoBackAsync();
        }
    }
}
