using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.MSAL
{
    public static class Constants
    {
        public static readonly string TenantName = "AuthASPAngular";
        public static readonly string TenantId = "AuthASPAngular.onmicrosoft.com";
        public static readonly string ClientId = "52810140-36e8-4d31-a3c2-243a5aa3a389";
        public static readonly string SignInPolicy = "b2c_1_sisu_xamarin";
        public static readonly string IosKeychainSecurityGroups = "com.mi8.aadb2cauthentication"; // e.g com.contoso.aadb2cauthentication
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
    }
}

