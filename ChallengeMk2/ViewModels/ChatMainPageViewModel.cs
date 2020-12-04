using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.AppModel;
using Prism.Navigation;
using ChallengeMk2.Services;
using ChallengeMk2.Models.ChatModels;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChallengeMk2.ViewModels
{
    public class ChatMainPageViewModel : PrismBaseViewModel, IPageLifecycleAware
    {
        readonly IChatService chatService;

        public ChatMainPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Discussions";
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
            chatService.Connection.Closed += async (error) =>
            {
                chatService.IsConnected = false;
                //TODO Warn user that he is disconnected => "Connection closed - Try to reconnect in few seconds..."
                var random = new Random();
                await Task.Delay(random.Next(0, 5) * 1000);
                await chatService.ConnectAsync();
            };

            chatService.Connection.On<MessageSentForm>("receiveNewPublicMessage", message =>
            {
                PublicMessages.Add(message);
            });

            //// Connect it
            //if (chatService.Connection.State == HubConnectionState.Disconnected)
            //{
            //    await chatService.ConnectAsync();
            //}
        }

        async Task SendPublicMessageAsync()
        {
            await chatService.SendPublicMessageAsync(EntryPublicMessage);
            EntryPublicMessage = "";
        }
    }
}
