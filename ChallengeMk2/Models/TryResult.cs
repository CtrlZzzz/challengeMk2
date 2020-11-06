using System.Net;

namespace ChallengeMk2.Models
{
    public class TryResult
    {
        public TryResult()
        {

        }
        public TryResult(TryResult partialTryResult, HttpStatusCode status, int userTry)
        {
            TryNumber = partialTryResult.TryNumber;
            Result = partialTryResult.Result;
            Status = status;
            UserTry = userTry;
        }
        public TryResult(int tryNumber, string result, HttpStatusCode status, int userTry)
        {
            TryNumber = tryNumber;
            Result = result;
            Status = status;
            UserTry = userTry;
        }

        public int TryNumber { get; set; }
        public string Result { get; set; }
        public HttpStatusCode Status { get; set; }
        public int UserTry { get; set; }
    }
}
