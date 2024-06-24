namespace Zparta.Data
{
    public class PlayerProgress
    {
        public int Gold { get; set; }
        public int LastUnlockedLevel { get; set; }
        
       
        private int _gold;
        private int _lastUnlockedLevel;
        //TODO private float _wealopSizeBonus;

        
        public PlayerProgress(int gold, int lastUnlockedLevel)
        {
            _gold = gold;
            _lastUnlockedLevel = lastUnlockedLevel;
        }
    }
}