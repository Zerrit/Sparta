using System;

namespace Zparta.ScoreLogic
{
    public interface IScoreListener
    {
        public event Action<int> OnScoreChange;
        public event Action<int> OnScoreIncrease;
        public event Action<int> OnComboChange;

        public int Score { get; }
    }
}