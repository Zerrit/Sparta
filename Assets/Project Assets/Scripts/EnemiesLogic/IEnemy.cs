using System;

namespace Zparta.EnemiesLogic
{
    public interface IEnemy
    {
        event Action<Enemy> OnKilled;

        int RewardValue { get; }

        void Activate();
        void Kill();
    }
}