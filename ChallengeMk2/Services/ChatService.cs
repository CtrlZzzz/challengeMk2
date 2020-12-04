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
        const string AccountRoute = "/api/account";
        const string LoginRoute = "/api/account/login/";
        const string ConnectionBuildRoute = "/chat?userId=";
        const string PublicChatRoute = "/api/chat/public";
        const string SendPublicChatRoute = "/api/chat/public/send";

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

        public async Task SendPublicMessageAsync(string message)
        {
            var url = ChatRootUrl + SendPublicChatRoute;

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = ConvertToSentForm(message);
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

        MessageSentForm ConvertToSentForm(string message)
        {
            return new MessageSentForm(ConnectedUser.Id, ConnectedUser.Username, message);
        }
    }
}
