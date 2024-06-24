using System;

namespace Zparta.Levels.BonusLevel
{
    public interface IBonusLevelHandler
    {
        public event Action OnBonusGameStarted;
        public event Action OnBonusGameFinished;
        public event Action<float> OnResultReceived;

        public void StartBonusLevel();
        public void FinishBonusLevel();
    }
}