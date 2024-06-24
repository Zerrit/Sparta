using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Zparta.ScoreLogic;
using Zparta.Services;
using Zparta.WalletLogic;
using Zparta.Windows.BonusGame;

namespace Zparta.Levels.BonusLevel
{
    public class BonusLevelController_RandomCards : BonusLevelController, IBonusLevelController_RandomCards_Window
    {
        [SerializeField] GameObject m_bonusLevelObject;
        [SerializeField] BonusPlayer m_player;
        [SerializeField] BonusEnemy m_enemy;
        [SerializeField] float m_finishBonusLevelDelay = 2;
        
        IScoreListener _scoreListener;
        BonusGameWindow_RandomCards _window;
        IRewardable _rewardable;
        BonusEnemy _currentEnemy;



        [Inject]
        public void Construct(BonusGameWindow_RandomCards window, IScoreListener scoreListener, IRewardable rewardable)
        {
            _window = window;
            _scoreListener = scoreListener;
            _rewardable = rewardable;
        }



        public override void StartGame()
        {
            _window.SetController(this);
            _window.Show();
        }

        public override void StopGame()
        {
            _window.Hide();
            HideBonusLevel();
        }

        void ShowBonusLevel()
        {
            _currentEnemy = m_enemy; // TODO Выбирать рандомно из списка всех доступных мобов.
            _currentEnemy.gameObject.SetActive(true);
            _currentEnemy.ResetObject();
            m_bonusLevelObject.SetActive(true);

            m_player.StartPreparing();
        }

        void HideBonusLevel()
        {
            m_bonusLevelObject.SetActive(false);
        }

        void PunchEnemy(float value)
        {
            _currentEnemy.Punch(value);
            _currentEnemy.OnStopped += OnEnenmyStopped;
        }

        void OnEnenmyStopped()
        {
            _currentEnemy.OnStopped -= OnEnenmyStopped;
            StartCoroutine(FinishBonusLevelDelay());
        }

        IEnumerator Punch(float value)
        {
            m_player.Punch();

            yield return new WaitForSeconds(0.25f);

            PunchEnemy(value);
        }

        IEnumerator FinishBonusLevelDelay()
        {
            yield return new WaitForSeconds(m_finishBonusLevelDelay);
            CallFinishEvent();
            Debug.LogWarning("Враг остановился");
            //_bonusLevelHandler.FinishBonusLevel();
        }



        public override void Activate()
        {
            //_bonusLevelHandler.OnBonusGameStarted += OnActivate;
            //_bonusLevelHandler.OnBonusGameFinished += OnDeactivate;

            ShowBonusLevel();
        }

        public override void Diactivate()
        {
            //_bonusLevelHandler.OnBonusGameStarted -= OnActivate;
            //_bonusLevelHandler.OnBonusGameFinished -= OnDeactivate;

            HideBonusLevel();
        }

        public void ReciveBonusInfo(BonusInfo bonusInfo)
        {
            switch (bonusInfo.Type)
            {
                case BonusInfo.BonusType.None:
                    break;

                case BonusInfo.BonusType.Money:
                    _rewardable.Reward((int)bonusInfo.Value);
                    break;
            }

            StartCoroutine(Punch(bonusInfo.PunchDistance));
        }
    }

    public interface IBonusLevelController_RandomCards_Window
    {
        public void ReciveBonusInfo(BonusInfo bonusInfo);
    }
}