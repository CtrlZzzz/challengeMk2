using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChallengeMk2.Models.ChatModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChallengeMk2.Services
{
    public interface IChatService
    {
        HubConnection Connection { get; set; }

        bool IsConnected { get; set; }

        User ConnectedUser { get; set; }

        Task<LoginResult> LoginAsync(string userName, string userPassword);

        Task ConnectAsync();

        Task DisconnectAsync();

        Task<LoginResult> CreateAccountAsync(string userName, string password, string firstName, string lastName);

        Task SendPublicMessageAsync(string message);

        Task<List<MessageSentForm>> GetAllPublicMessagesAsync();

        Task<List<UserListObject>> GetAllUsersAsync();

        Task<List<RoomListObject>> GetAllRoomsAsync();

        Task<User> GetUserAsync(string userId);

        Task UpdateUserInfoAsync();

        Task<Room> GetRoomAsync(string id);

        Task<LoginResult> CreateNewRoomAsync(string roomName);

        Task JoinRoomAsync(string roomId, string roomName);

        Task QuitRoomAsync(string roomId);

        Task SendRoomMessageAsync(string message, string roomId);

        Task AddContactAsync(string contactId, string contactName);

        Task SendPrivateMessageAsync(string message, string receiverId);
    }
}
