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

        Task SendPublicMessageAsync(string message);
    }
}
