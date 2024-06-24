using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zparta.Interfaces;
using Zparta.Levels.MainLevel;
using Zparta.Levels.Model;
using Zparta.Services;
using Zparta.Windows.Victory.Window;

namespace Zparta.Windows.Victory.Controller
{
    public class VictoryController : IVictoryController
    {
        public event Action OnContinueClick;

        private readonly VictoryWindow _victoryWindow;
        private readonly IReadOnlyLevelModel _levelModel;
        private readonly ICoroutineRunner _coroutineRunner;

        public VictoryController(VictoryWindow victoryWindow, IReadOnlyLevelModel levelModel, ICoroutineRunner coroutineRunner)
        {
            _victoryWindow = victoryWindow;
            _levelModel = levelModel;
            _coroutineRunner = coroutineRunner;
        }


        public void Open()
        {
            SetResults();
            _victoryWindow.Show();
            _coroutineRunner.StartCoroutine(ShowStars(_levelModel.GetEarnedStarsAmount()));
            
            _victoryWindow.ContinueButton.onClick.AddListener(Close);
        }

        public void Close()
        {
            _victoryWindow.ContinueButton.onClick.RemoveListener(Close);
            
            _victoryWindow.Hide();
            OnContinueClick?.Invoke();
        }

        private void SetResults()
        {
            _victoryWindow.PushedEnemiesText.text = $"{_levelModel.PushedEnemiesPercent.ToString()}%";
            _victoryWindow.TotalScoreText.text = _levelModel.Score.ToString();
            _victoryWindow.EarnedCoins.text = _levelModel.Coins.ToString();
        }
        
        private IEnumerator ShowStars(int amount)
        {
            yield return new WaitForSeconds(.5f);
            
            for (int i = 0; i < amount; i++)
            {
                _victoryWindow.StarImages[i].color = Vector4.one;
                _victoryWindow.StarImages[i].rectTransform.DOPunchScale(new Vector3(1.5f, 1.5f, 1f), .5f, 2);
                yield return new WaitForSeconds(.8f);
            }
        }
    }
}