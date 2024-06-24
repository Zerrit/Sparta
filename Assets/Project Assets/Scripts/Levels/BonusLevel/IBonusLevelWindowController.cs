using System;

namespace Zparta.Levels.BonusLevel
{
    public interface IBonusLevelWindowController
    {
        public event Action<float> OnChargeChanged;
        public event Action<float> OnResultReceived;

        public void FinishBonusLevel();
    }
}