using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Zparta.ScoreLogic;
using Zparta.Services;

namespace Zparta.Levels.BonusLevel
{
    public class BonusLevel_Old : MonoBehaviour, IBonusLevelHandler, IBonusLevelWindowController
    {
        public event Action OnBonusGameStarted;
        public event Action OnBonusGameFinished;
        public event Action<float> OnResultReceived;
        public event Action<float> OnChargeChanged;

        [SerializeField] private GameObject bonusLevelObject;
        [SerializeField] private BonusPlayer player;
        [SerializeField] private BonusEnemy_Physic enemy;

        private float _charge;
        
        private IScoreListener _scoreListener;
       // private IWindowService _windowService;
        private ZpartaInput _input;
        
        
        /*[Inject]
        public void Construct(IWindowService windowService, IScoreListener scoreListener)
        {
            _input = new ZpartaInput();
            _windowService = windowService;
            _scoreListener = scoreListener;
            _input.Player.Attack.performed += _ => ChargePunch();

            HideBonusLevel();
        }*/


        public void StartBonusLevel()
        {
            ShowBonusLevel();
            //_windowService.ShowWindow("BonusGame");
            EnableCharging();

            OnBonusGameStarted?.Invoke();
        }
        
        public void FinishBonusLevel()
        {
            OnBonusGameFinished?.Invoke();
            HideBonusLevel();
           // _windowService.HideWindow("BonusGame");
        }
        
        
        /// <summary>
        /// Включает отображение бонусного уровня и подготавливает его.
        /// </summary>
        private void ShowBonusLevel()
        {
            bonusLevelObject.SetActive(true);
            enemy.ResetObject();
        }
        
        /// <summary>
        /// Выключает отображение бонусного уровня.
        /// </summary>
        private void HideBonusLevel()
        {
            bonusLevelObject.SetActive(false);
        }

        private void EnableCharging()
        {
            _charge = 0;
            _input.Enable();
            StartCoroutine(ChargeReducer());
        }
        
        /// <summary>
        /// Увеличивает заряд для удара. А при полном заряде толкает врага.
        /// </summary>
        private void ChargePunch()
        {
            _charge += 0.05f;

            if (_charge >= 1)
            {
                _input.Disable();
                player.Punch();
                enemy.Punch(GetPunchForceFromScore(_scoreListener.Score));
                enemy.OnStoppedWithScore += HandleResult;
            }
        }
        
        /// <summary>
        /// Обрабатывает результаты бонусного уровня.
        /// </summary>
        /// <param name="value"> Полученный множдитель. </param>
        private void HandleResult(float value)
        {
            enemy.OnStoppedWithScore -= HandleResult;
            OnResultReceived?.Invoke(value);
        }

        private int GetPunchForceFromScore(int score)
        {
            return score / 5;
        }
        
        /// <summary>
        /// Корутина убавляет значение заряда до тех пока он не заполнится полностью.
        /// </summary>
        private IEnumerator ChargeReducer()
        {
            while (_charge < 1)
            {
                if (_charge > 0)
                {
                    _charge -= .03f * Time.deltaTime;
                    OnChargeChanged?.Invoke(_charge);
                }
                yield return null;
            }
            
            Debug.LogWarning("Корутина прекратилась");
        }
    }
}
