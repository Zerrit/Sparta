using System;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public abstract class BonusEnemy : MonoBehaviour
    {
        public abstract void Punch(float finalValue);
        public abstract void Explode();
        public abstract void ResetObject();

        public abstract event Action OnStopped;
    }
}