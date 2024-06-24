using System;
using UnityEngine;
using Zparta.Levels.Model;
using Zparta.ScoreLogic;

namespace Zparta.Windows.Gameplay
{
    public class GameplayWindowController : IGameplayWindowController
    {
        public event Action OnExit;
        
        private readonly IReadOnlyLevelModel _levelModel;
        private readonly IScoreListener _scoreListener;
        private readonly GameplayWindow _gameplayWindow;
        
        
        public GameplayWindowController(IReadOnlyLevelModel levelModel, IScoreListener scoreListener, GameplayWindow gameplayWindow)
        {
            _levelModel = levelModel;
            _scoreListener = scoreListener;
            _gameplayWindow = gameplayWindow;
        }

        public void Show()
        {
            _gameplayWindow.Show();

            _gameplayWindow.LevelNumberText.text = $"{_levelModel.CurrentLevel + 1}";
            InitializeLevelScheme();
            
            _scoreListener.OnScoreChange += UpdateScore;
            _scoreListener.OnComboChange += _gameplayWindow.UpdateComboView;
            _levelModel.OnStageCleared += _gameplayWindow.ShowStageClearNotification;
            _levelModel.OnPowerUpPicked += _gameplayWindow.ShowPowerUpNotification;
            _levelModel.OnStageFinished += UpdateActiveSegment;
            _gameplayWindow.HomeButton.onClick.AddListener(ShowHomeRequestPanel);
        }

        public void Hide()
        {
            _scoreListener.OnScoreChange -= UpdateScore;
            _scoreListener.OnComboChange -= _gameplayWindow.UpdateComboView;
            _levelModel.OnStageCleared -= _gameplayWindow.ShowStageClearNotification;
            _levelModel.OnPowerUpPicked -= _gameplayWindow.ShowPowerUpNotification;
            _levelModel.OnStageFinished -= UpdateActiveSegment;
            _gameplayWindow.HomeButton.onClick.RemoveListener(ShowHomeRequestPanel);
        
            _gameplayWindow.Hide();
        }

        /// <summary>
        /// Обновляет отображение набранных очков.
        /// </summary>
        /// <param name="score"> Новое значение. </param>
        private void UpdateScore(int score)
        {
            _gameplayWindow.ScoreText.text = score.ToString();
        }

        private void InitializeLevelScheme()
        {
            _gameplayWindow.LevelSchemeView.UnselectAll();
            DisplaySegments(_levelModel.StageCount);
            UpdateActiveSegment(0);
        }

        private void DisplaySegments(int amount)
        {
            for (int i = 0; i < amount - 1; i++)
            {
                _gameplayWindow.LevelSchemeView.Segments[i].gameObject.SetActive(true);
            }
        }

        private void UpdateActiveSegment(int index)
        {
            if (index > 0)
            {
                var lastSegment = _gameplayWindow.LevelSchemeView.Segments[index - 1];
                _gameplayWindow.LevelSchemeView.Complete(lastSegment);
            }

            if (index == _levelModel.StageCount -1)
            {
                _gameplayWindow.LevelSchemeView.SelectFinal();
            }
            else
            {
                var segment = _gameplayWindow.LevelSchemeView.Segments[index];
                _gameplayWindow.LevelSchemeView.Select(segment);
            }
        }

        private void ShowHomeRequestPanel()
        {
            Time.timeScale = 0;
            _gameplayWindow.HomeRequestPanel.SetActive(true);
            
            _gameplayWindow.ConfirmHome.onClick.AddListener(ReturnToMenu);
            _gameplayWindow.CancelHome.onClick.AddListener(HideRequestPanel);
        }

        private void HideRequestPanel()
        {
            _gameplayWindow.ConfirmHome.onClick.RemoveListener(ReturnToMenu);
            _gameplayWindow.CancelHome.onClick.RemoveListener(HideRequestPanel);
            
            Time.timeScale = 1;
            _gameplayWindow.HomeRequestPanel.SetActive(false);
        }

        private void ReturnToMenu()
        {
            _gameplayWindow.ConfirmHome.onClick.RemoveListener(ReturnToMenu);
            _gameplayWindow.CancelHome.onClick.RemoveListener(HideRequestPanel);
            
            Time.timeScale = 1;
            _gameplayWindow.HomeRequestPanel.SetActive(false);
            OnExit?.Invoke();
        }
    }
}