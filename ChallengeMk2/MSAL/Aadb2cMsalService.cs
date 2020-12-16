using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace ChallengeMk2.MSAL
{
    class Aadb2cMsalService : IAuthenticationService
    {
        IPublicClientApplication authenticationClient;


        public AuthenticationResult Authentication { get; set; }


        public Aadb2cMsalService()
        {
            //InitializeAuthenticationService();
        }


        public async Task<IEnumerable<IAccount>> GetAccountsAsync()
        {
            return await authenticationClient.GetAccountsAsync();
        }

        public async Task SignInWithAccountAsync(IAccount existingAccount)
        {
            Authentication = await authenticationClient
                .AcquireTokenSilent(Constants.Scopes, existingAccount)
                .ExecuteAsync();
        }

        public async Task SignInAsync()
        {
            Authentication = await authenticationClient
                .AcquireTokenInteractive(Constants.Scopes)
                .WithPrompt(Prompt.ForceLogin)
                .WithParentActivityOrWindow(App.UIParent)
                .ExecuteAsync();
        }

        public async Task AutoSignIn()
        {
            var accounts = await GetAccountsAsync();

            if (accounts.Count() >= 1)
            {
                await SignInWithAccountAsync(accounts.FirstOrDefault());
            }
            else
            {
                await SignInAsync();
            }
        }
        void InitializeAuthenticationService()
        {
            authenticationClient = PublicClientApplicationBuilder.Create(Constants.ClientId)
               .WithIosKeychainSecurityGroup(Constants.IosKeychainSecurityGroups)
               .WithB2CAuthority(Constants.AuthoritySignIn)
               .WithRedirectUri($"msal{Constants.ClientId}://auth")
               .Build();
        }
    }
}
