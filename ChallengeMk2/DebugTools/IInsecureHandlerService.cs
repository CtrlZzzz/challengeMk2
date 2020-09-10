using System.Net.Http;

namespace ChallengeMk2.DebugTools
{
    public interface IInsecureHandlerService
    {
        HttpClientHandler GetInsecureHanler();
    }
}
