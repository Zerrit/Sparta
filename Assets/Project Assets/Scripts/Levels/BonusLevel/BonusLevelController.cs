using System;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public abstract class BonusLevelController : MonoBehaviour
    {
        public event Action OnFinished;
        
        public abstract void Activate();
        public abstract void Diactivate();

        public abstract void StartGame();
        public abstract void StopGame();

        protected void CallFinishEvent()
            => OnFinished?.Invoke();
    }
}