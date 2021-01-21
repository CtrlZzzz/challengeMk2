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
    public class ChatPublicPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize
    {
        readonly IChatService chatService;

        public ChatPublicPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Public chat";
            chatService = chat;
            PublicMessages = new ObservableCollection<MessageSentForm>();
            SendPublicMessageCommand = new Command(async () => await SendPublicMessageAsync());
        }


        string entryPublicMessage;
        public string EntryPublicMessage
        {
            get => entryPublicMessage;
            set => SetProperty(ref entryPublicMessage, value);
        }

        ObservableCollection<MessageSentForm> publicMessages;
        public ObservableCollection<MessageSentForm> PublicMessages
        {
            get { return publicMessages; }
            set { SetProperty(ref publicMessages, value); }
        }

        public Command SendPublicMessageCommand { get; set; }

        public void OnAppearing() => InitializeConnection();
        public void OnDisappearing()
        {
        }

        void InitializeConnection()
        {
            chatService.Connection.On<MessageSentForm>("receiveNewPublicMessage", message =>
            {
                PublicMessages.Add(message);
            });
        }

        async Task SendPublicMessageAsync()
        {
            await chatService.SendPublicMessageAsync(EntryPublicMessage);
            EntryPublicMessage = "";
        }
    }
}
