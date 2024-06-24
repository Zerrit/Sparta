using System;
using UnityEngine;

namespace Zparta.Levels.BonusLevel 
{
    public class BonusLevelHandler : MonoBehaviour, IBonusLevelHandler
    {
        [SerializeField] BonusLevelController[] m_bonusLevelControllers;

        public event Action OnBonusGameStarted;
        public event Action OnBonusGameFinished;
        public event Action<float> OnResultReceived;
        

        void ActivateRandomController()
        {
            m_bonusLevelControllers[0].Activate(); //TODO переделать на рандомный выбор из массива.
        }

        public void FinishBonusLevel()
        {
            Debug.LogWarning("Завершаем уровень");
            m_bonusLevelControllers[0].OnFinished -= FinishBonusLevel;
            
            m_bonusLevelControllers[0].StopGame();
            m_bonusLevelControllers[0].Diactivate();
            OnBonusGameFinished?.Invoke();
        }

        public void StartBonusLevel()
        {
            ActivateRandomController();
            m_bonusLevelControllers[0].OnFinished += FinishBonusLevel;
            m_bonusLevelControllers[0].StartGame();
        }
    }
}