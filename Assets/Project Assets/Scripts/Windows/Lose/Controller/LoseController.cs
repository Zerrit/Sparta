using System;
using System.Collections;
using UnityEngine;
using Zparta.Interfaces;
using Zparta.Levels.MainLevel;
using Zparta.Services;
using Zparta.Windows.Lose.Window;

namespace Zparta.Windows.Lose.Controller
{
    public class LoseController : ILoseController
    {
        public event Action OnRestart;
        public event Action OnResurrected;

        private const float RenewTime = 7f;
        private const float EarlyRestartTime = 3f;

        private Coroutine _timerCoroutine;
        
        private readonly LoseWindow _loseWindow;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IMainLevelHandler _levelHandler;

        public LoseController(LoseWindow loseWindow, ICoroutineRunner coroutineRunner, IMainLevelHandler levelHandler)
        {
            _loseWindow = loseWindow;
            _coroutineRunner = coroutineRunner;
            _levelHandler = levelHandler;
        }
        

        public void Start()
        {
            _loseWindow.Show();
            ShowResurrectionButton();
        }

        public void Close()
        {
            _loseWindow.Hide();

            _coroutineRunner.StopCoroutine(_timerCoroutine);
            OnRestart?.Invoke();

            _loseWindow.RestartButton.onClick.RemoveListener(Close);
            _loseWindow.RenewView.AdButton.onClick.RemoveListener(StartAdAndResurrect);
            _loseWindow.RenewView.Restart.onClick.RemoveListener(Close);
        }

        private void ShowResurrectionButton()
        {
            _loseWindow.RestartButton.gameObject.SetActive(false);
            _loseWindow.RenewView.gameObject.SetActive(true);
            _loseWindow.RenewView.Restart.gameObject.SetActive(false);

            _timerCoroutine = _coroutineRunner.StartCoroutine(AdRenewTimer());
            
            _loseWindow.RenewView.AdButton.onClick.AddListener(StartAdAndResurrect);
            _loseWindow.RenewView.Restart.onClick.AddListener(Close);
        }

        private void StartAdAndResurrect()
        {
            //Show AD
            _levelHandler.InitializeCurrentStage();
            
            OnResurrected?.Invoke();
            Close();
        }

        private IEnumerator AdRenewTimer()
        {
            float timer = 0;

            while (timer <= RenewTime)
            {
                _loseWindow.RenewView.Slider.value = timer/RenewTime;
                timer += Time.deltaTime;

                if (timer >= EarlyRestartTime)
                {
                    _loseWindow.RenewView.Restart.gameObject.SetActive(true);
                }

                yield return null;
            }
            
            _loseWindow.RenewView.gameObject.SetActive(false);
            _loseWindow.RestartButton.gameObject.SetActive(true);
            _loseWindow.RestartButton.onClick.AddListener(Close);
        }
    }
}