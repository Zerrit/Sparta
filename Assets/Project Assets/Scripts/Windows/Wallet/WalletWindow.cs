using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zparta.Services;
using Zparta.WalletLogic;

namespace Zparta.Windows.Wallet
{
    public class WalletWindow : AbstractWindow
    {
        [SerializeField] private Text goldText;

        private IWallet _wallet;
        
        
        [Inject]
        public void Construct(IWallet wallet)
        {
            _wallet = wallet;
        }

        private void Awake()
        {
            Show();
        }

        public override void Show()
        {
            base.Show();

            UpdateGold(_wallet.Gold);
            _wallet.OnGoldChange += UpdateGold;
        }

        public override void Hide()
        {
            _wallet.OnGoldChange += UpdateGold;
            
            base.Hide();
        }
        
        
        private void UpdateGold(int value)
        {
            goldText.text = value.ToString();
        }
    }
}