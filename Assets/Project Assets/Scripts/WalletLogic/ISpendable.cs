namespace Zparta.WalletLogic
{
    public interface ISpendable
    {
        public bool CheckPurchaseAbility(int cost);
        public bool TrySpend(int value);
    }
}