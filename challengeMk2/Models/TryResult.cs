using System;
using System.Net;

namespace ChallengeMk2.Models
{
    public class TryResult
    {
        public int TryNumber { get; set; }
        public string Result { get; set; }
        public HttpStatusCode Status { get; set; }
        public int UserTry { get; set; }
    }
}
