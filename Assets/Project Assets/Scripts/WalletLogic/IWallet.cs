using System;

namespace Zparta.WalletLogic
{
    public interface IWallet
    {
        public event Action<int> OnGoldChange;
        public int Gold { get; }
    }
}