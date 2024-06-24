using System;
using System.Collections;
using UnityEngine;
using Zparta.Interfaces;

namespace Zparta.ScoreLogic
{
    public class ScoreSystem : IScoreListener, IScoreChanger
    {
        public event Action<int> OnScoreChange;
        public event Action<int> OnScoreIncrease;
        public event Action<int> OnComboChange;

        public int Score { get; private set; }

        private bool _hasCombo;
        private int _currentCombo;
        private int _baseComboValue = 1;
        private float _lastScoreUpdateTime;
        private float _comboMultiplierStep = .5f;
        private float _comboTime = 1.7f;
        
        private Coroutine _comboTimer;
        private readonly ICoroutineRunner _coroutineRunner;

        
        public ScoreSystem(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        
        public void ChangeScore(int value)
        {
            Score = value;
            OnScoreChange?.Invoke(Score);
        }
        

        public void IncreaseScore(int value)
        {
            _currentCombo++;
            _lastScoreUpdateTime = Time.time;
            int addition = (int)(value * _comboMultiplierStep * _currentCombo);
            Score += addition;
            
            OnScoreIncrease?.Invoke(addition);
            OnScoreChange?.Invoke(Score);
            OnComboChange?.Invoke(_currentCombo);
            
            if (!_hasCombo)
            {
                _comboTimer = _coroutineRunner.StartCoroutine(ComboTimer());
            }
        }
        
        private IEnumerator ComboTimer()
        {
            _hasCombo = true;
            
            while (Time.time < _lastScoreUpdateTime + _comboTime)
            {
                yield return null;
            }
            _currentCombo = _baseComboValue;
            _hasCombo = false;
            OnComboChange?.Invoke(0);
        }
    }
}