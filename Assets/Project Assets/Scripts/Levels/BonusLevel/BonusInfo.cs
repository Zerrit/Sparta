using System;

namespace Zparta.Levels.BonusLevel
{
    [Serializable]
    public class BonusInfo
    {
        public enum BonusType
        {
            None,
            Money,
        }

        public float PunchDistance;
        public BonusType Type;
        public float Value;
    }
}