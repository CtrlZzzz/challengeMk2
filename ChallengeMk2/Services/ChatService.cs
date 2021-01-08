using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChallengeMk2.Services;
using Newtonsoft.Json;
using ChallengeMk2.Models.ChatModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChallengeMk2.Services
{
    class ChatService : IChatService
    {
        const string ChatRootUrl = "https://chat-prototype-api.azurewebsites.net";
        const string AccountRoute = "/api/account/";
        const string LoginRoute = "/api/account/login/";
        const string ConnectionBuildRoute = "/chat?userId=";
        const string PublicChatRoute = "/api/chat/public";
        const string SendPublicChatRoute = "/api/chat/public/send";
        const string RoomChatRoute = "/api/chat/room/";
        const string NewRoomRoute = "/api/chat/room/new";
        const string PrivateChatRoute = "/api/chat/private/";

        public HubConnection Connection { get; set; }

        public bool IsConnected { get; set; }

        public User ConnectedUser { get; set; }


        public async Task<LoginResult> LoginAsync(string userName, string userPassword)
        {
            LoginResult result;

            var url = ChatRootUrl + LoginRoute + userName;

            using var client = new HttpClient();
            using var response = await client.GetAsync(url).ConfigureAwait(false);
            var responseData = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = JsonConvert.DeserializeObject<User>(responseData);

                if (user.Password == userPassword)
                {
                    result = new LoginResult(true, user);
                    InitializeConnection(user.Id);
                    await ConnectAsync();
                    ConnectedUser = user;
                }
                else
                {
                    result = new LoginResult(false, "Incorrect password");
                }
            }
            else
            {
                result = response.StatusCode == HttpStatusCode.NotFound
                    ? new LoginResult(false, "Incorrect username")
                    : new LoginResult(false, "Internal server error");
            }

            return result;
        }

        public async Task ConnectAsync()
        {
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync();
                IsConnected = true;
            }
        }

        public async Task DisconnectAsync()
        {
            await Connection.StopAsync();
            IsConnected = false;
        }


        public async Task<LoginResult> CreateAccountAsync(string userName, string password, string firstName, string lastName)
        {
            LoginResult result;

            var url = ChatRootUrl + AccountRoute;

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new RegisterForm(userName, password, firstName, lastName);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
            var responseData = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var user = JsonConvert.DeserializeObject<User>(responseData);

                result = new LoginResult(true, user);
                InitializeConnection(user.Id);
                await ConnectAsync();
                ConnectedUser = user;
            }
            else
            {
                result = response.StatusCode == HttpStatusCode.BadRequest
                    ? new LoginResult(false, "Username already exists")
                    : new LoginResult(false, "Internal server error");
            }

            return result;
        }

        public async Task SendPublicMessageAsync(string message)
        {
            var url = ChatRootUrl + SendPublicChatRoute;

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = ConvertStringToSentForm(message);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
        }

        public async Task<List<MessageSentForm>> GetAllPublicMessagesAsync()
        {
            var url = ChatRootUrl + PublicChatRoute;

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var responseData = JsonConvert.DeserializeObject<List<Message>>(response);

            //TODO => move this in a separate mapper service?
            var mappedData = new List<MessageSentForm>();
            foreach (var item in responseData)
            {
                mappedData.Add(new MessageSentForm(item.SenderId, item.SenderName, item.Content));
            }

            return mappedData;
        }

        public async Task<List<UserListObject>> GetAllUsersAsync()
        {
            var url = ChatRootUrl + AccountRoute;

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var responseData = JsonConvert.DeserializeObject<List<UserListObject>>(response);

            return responseData;
        }

        public async Task<List<RoomListObject>> GetAllRoomsAsync()
        {
            var url = ChatRootUrl + RoomChatRoute;

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var responseData = JsonConvert.DeserializeObject<List<RoomListObject>>(response);

            return responseData;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var url = ChatRootUrl + AccountRoute + userId;

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var responseData = JsonConvert.DeserializeObject<User>(response);

            return responseData;
        }

        public async Task UpdateUserInfoAsync()
        {
            ConnectedUser = await GetUserAsync(ConnectedUser.Id);
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            var url = ChatRootUrl + RoomChatRoute + roomId;

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var responseData = JsonConvert.DeserializeObject<Room>(response);

            return responseData;
        }

        public async Task<LoginResult> CreateNewRoomAsync(string roomName)
        {
            LoginResult result;

            var url = ChatRootUrl + NewRoomRoute;

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new NewRoomForm(roomName);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
            var responseData = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var newRoom = JsonConvert.DeserializeObject<Room>(responseData);

                result = new LoginResult(true, newRoom.Id);

                await JoinRoomAsync(newRoom.Id, newRoom.RoomName);
            }
            else
            {
                result = response.StatusCode == HttpStatusCode.BadRequest
                    ? new LoginResult(false, "Room already exists")
                    : new LoginResult(false, "Internal server error");
            }

            return result;
        }

        public async Task JoinRoomAsync(string roomId, string roomName)
        {
            var url = ChatRootUrl + RoomChatRoute + roomId + "/join";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new JoinRoomForm(roomName, ConnectedUser.Id, ConnectedUser.Username);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);

            await UpdateUserInfoAsync();
        }

        public async Task QuitRoomAsync(string roomId)
        {
            var url = ChatRootUrl + RoomChatRoute + roomId + "/" + ConnectedUser.Id + "/quit";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            //var content = new JoinRoomForm(roomName, ConnectedUser.Id, ConnectedUser.Username);
            //var jsonContent = JsonConvert.SerializeObject(content);
            //using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
            //DEBUG
            var debug = await response.Content.ReadAsStringAsync();

            await UpdateUserInfoAsync();
        }

        public async Task SendRoomMessageAsync(string message, string roomId)
        {
            var url = ChatRootUrl + RoomChatRoute + roomId + "/send";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = ConvertStringToSentForm(message);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
        }

        public async Task AddContactAsync(string contactId, string contactName)
        {
            var url = ChatRootUrl + PrivateChatRoute + ConnectedUser.Id + "/new";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new NewContactForm(ConnectedUser.Username, contactId, contactName);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
            var debug = await response.Content.ReadAsStringAsync();

            await UpdateUserInfoAsync();
        }

        public async Task SendPrivateMessageAsync(string message, string receiverId)
        {
            var url = ChatRootUrl + PrivateChatRoute + receiverId + "/send";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = ConvertStringToSentForm(message);
            var jsonContent = JsonConvert.SerializeObject(content);
            using var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = stringContent;
            using var response = await client.SendAsync(request).ConfigureAwait(false);
        }

        void InitializeConnection(string userId)
        {
            var url = ChatRootUrl + ConnectionBuildRoute + userId;

            Connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }

        MessageSentForm ConvertStringToSentForm(string message)
        {
            return new MessageSentForm(ConnectedUser.Id, ConnectedUser.Username, message);
        }

    }
}
