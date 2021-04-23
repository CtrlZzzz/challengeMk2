using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeMk2.MSAL
{
    public static class Constants
    {
        //public static readonly string TenantName = "AuthASPAngular";
        //public static readonly string TenantId = "AuthASPAngular.onmicrosoft.com";
        //public static readonly string ClientId = "52810140-36e8-4d31-a3c2-243a5aa3a389";
        //public static readonly string SignInPolicy = "b2c_1_sisu_xamarin";
        //public static readonly string IosKeychainSecurityGroups = "com.mi8.aadb2cauthentication"; // e.g com.contoso.aadb2cauthentication
        //public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        //public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        //public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
        //public static readonly string TenantName = "mi8org";
        //public static readonly string TenantId = "020f9858-e8b3-40d4-9bcb-0197322e78a3";
        //public static readonly string ClientId = "82d1efe1-1556-4b2a-bc88-a5182b0204d5";
        //public static readonly string SignInPolicy = "b2c_1_signup-signin";
        //public static readonly string IosKeychainSecurityGroups = "com.mi8org.aadb2cauthentication"; // e.g com.contoso.aadb2cauthentication
        //public static readonly string[] Scopes = new string[] { "openid", "offline_access", "https://mi8org.onmicrosoft.com/318981ae-2aac-4a69-be66-779e58fc97d7/read" };
        //public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
        //public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
        public static readonly string TenantName = "mi8org.onmicrosoft.com";
        public static readonly string TenantId = "020f9858-e8b3-40d4-9bcb-0197322e78a3";
        public static readonly string ClientId = "82d1efe1-1556-4b2a-bc88-a5182b0204d5";
        public static readonly string SignInPolicy = "b2c_1_signup-signin";
        public static readonly string IosKeychainSecurityGroups = "com.mi8org.aadb2cauthentication"; // e.g com.contoso.aadb2cauthentication
        public static readonly string[] Scopes = new string[] { "https://mi8org.onmicrosoft.com/318981ae-2aac-4a69-be66-779e58fc97d7/read" };
        public static readonly string AuthorityBase = $"https://mi8org.b2clogin.com/tfp/{TenantName}/";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
    }
}

