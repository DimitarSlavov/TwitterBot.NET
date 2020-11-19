using System.ComponentModel;

namespace CryptoCore.Models.Common
{
    public class AccountBase : ModelBase
    {
        [DisplayName(nameof(Name))]
        public string Name { get; set; }
    }
}
