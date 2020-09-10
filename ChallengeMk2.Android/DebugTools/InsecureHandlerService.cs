using ChallengeMk2.DebugTools;
using System.Net.Http;

[assembly: Xamarin.Forms.Dependency(typeof(ChallengeMk2.Droid.DebugTools.InsecureHandlerService))]
namespace ChallengeMk2.Droid.DebugTools
{
    class InsecureHandlerService : IInsecureHandlerService
    {
        public HttpClientHandler GetInsecureHanler()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                    {
                        return true;
                    }

                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }
    }
}