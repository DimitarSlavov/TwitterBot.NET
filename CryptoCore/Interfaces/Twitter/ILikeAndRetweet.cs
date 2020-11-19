using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Twitter
{
    public interface ILikeAndRetweet
    {
        Task<bool> LikeAndRetweetWhitelistedAccount();
    }
}
