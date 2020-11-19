using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Common
{
	public interface IGenerateRandom
	{
		Task<double> GenerateRandomAsync();
	}
}
