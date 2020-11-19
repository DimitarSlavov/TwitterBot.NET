using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Common
{
	public interface IGetState
	{
		Task<bool> GetStateAsync();
	}
}
