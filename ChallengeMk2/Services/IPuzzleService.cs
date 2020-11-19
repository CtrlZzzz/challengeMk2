using System.Threading.Tasks;
using ChallengeMk2.Models;

namespace ChallengeMk2.Services
{
    public interface IPuzzleService
    {
        Task<TryResult> GetTryResult(int userTry);
    }
}
