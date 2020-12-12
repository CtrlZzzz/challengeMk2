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
    public class ChatAddRoomPageViewModel : PrismBaseViewModel, IPageLifecycleAware, IAutoInitialize
    {
        readonly IChatService chatService;

        public ChatAddRoomPageViewModel(INavigationService navigationService, IChatService chat) : base(navigationService)
        {
            Title = "Add new room";
            chatService = chat;
            SortedRooms = new ObservableCollection<RoomListObject>();
            ExistingRooms = new ObservableCollection<RoomListObject>();
            SearchCommand = new Command(() => Search());
            JoinRoomCommand = new Command<RoomListObject>(async (r) => await JoinRoomAsync(r));
        }

        string entrySearchMessage;
        public string EntrySearchMessage
        {
            get => entrySearchMessage;
            set => SetProperty(ref entrySearchMessage, value);
        }

        ObservableCollection<RoomListObject> existingRooms;
        public ObservableCollection<RoomListObject> ExistingRooms
        {
            get { return existingRooms; }
            set { SetProperty(ref existingRooms, value); }
        }

        ObservableCollection<RoomListObject> sortedRooms;
        public ObservableCollection<RoomListObject> SortedRooms
        {
            get { return sortedRooms; }
            set { SetProperty(ref sortedRooms, value); }
        }

        public Command SearchCommand { get; set; }
        public Command<RoomListObject> JoinRoomCommand { get; set; }

        public void OnAppearing() => InitializeViewModel();

        public void OnDisappearing()
        {
        }

        void InitializeViewModel()
        {
            var userRooms = chatService.ConnectedUser.Rooms;
            if (userRooms.Count == 0)
            {
                return;
            }

            //TODO => move this in a separate mapper service?
            var userRoomsMapped = new List<RoomListObject>();
            foreach (var item in userRooms)
            {
                userRoomsMapped.Add(new RoomListObject(item.RoomId, item.RoomName));
            }

            var copy = new List<RoomListObject>(existingRooms);
            foreach (var item in userRoomsMapped)
            {
                for (var i = 0; i < copy.Count; i++)
                {
                    if (copy[i].Id == item.Id)
                    {
                        ExistingRooms.Remove(copy[i]);
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
                SortedRooms = new ObservableCollection<RoomListObject>();
            }
            else
            {
                var searchedRooms = existingRooms.Where(u => u.RoomName.ToLower().StartsWith(EntrySearchMessage.ToLower()));
                SortedRooms = new ObservableCollection<RoomListObject>(searchedRooms);
            }
        }

        async Task JoinRoomAsync(RoomListObject newRoom)
        {
            try
            {
                IsBusy = true;

                await chatService.JoinRoomAsync(newRoom.Id, newRoom.RoomName);

                ExistingRooms.Remove(newRoom);

                var searchedRooms = existingRooms.Where(u => u.RoomName.ToLower().StartsWith(EntrySearchMessage.ToLower()));
                SortedRooms = new ObservableCollection<RoomListObject>(searchedRooms);
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
