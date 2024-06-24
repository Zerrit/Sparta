using System;
using Zparta.Services.PersistentData;
using Zparta.Windows.Wallet;

namespace Zparta.WalletLogic
{
    public class Wallet : IWallet, ISpendable, IRewardable
    {
        public event Action<int> OnGoldChange;
        
        public int Gold
        {
            get => _gold;
            private set
            {
                _gold = value;
                OnGoldChange?.Invoke(_gold);
            }
        }
        private int _gold;
        
        private readonly IProgressProvider _progressProvider;
        private readonly WalletWindow _walletWindow;


        public Wallet(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;

            _gold = _progressProvider.Progress.Gold;
        }

        
        /*
        public void Show()
        {
            _walletWindow.Show();
        }

        public void Hide()
        {
            _walletWindow.Hide();
        }*/
        
        
        public bool CheckPurchaseAbility(int cost)
        {
            return (Gold >= cost);
        }

        public bool TrySpend(int value)
        {
            if (!CheckPurchaseAbility(value)) return false;
            
            Gold -= value;
            return true;
        }

        public void Reward(int value) => Gold += value;
    }
}