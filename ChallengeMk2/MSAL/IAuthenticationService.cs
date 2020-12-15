using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace ChallengeMk2.MSAL
{
    public interface IAuthenticationService
    {
        AuthenticationResult Authentication { get; set; }


        Task<IEnumerable<IAccount>> GetAccountsAsync();

        Task SignInWithAccountAsync(IAccount existingAccount);

        Task SignInAsync();

        Task AutoSignIn();
    }
}
