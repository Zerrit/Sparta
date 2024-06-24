using System;

namespace Zparta.Levels.Model
{
    public interface IReadOnlyLevelModel
    {
        public event Action<int> OnLevelChanged;
        public event Action OnEnemyPushed;
        public event Action OnStageCleared;
        public event Action OnCoinPicked;
        public event Action OnPowerUpPicked;
        public event Action<int> OnStageFinished;
        
        public int LevelCount { get; }
        public int CurrentLevel { get; }
        public int CurrentStage { get; }
        public int StageCount { get; }
        
        public int[] StarsTresholds { get; }

        public int Coins { get; }
        public int Score { get; }
        public int PushedEnemiesPercent { get; }
        public int PushedEnemies { get; }
        public int TotalEnemiesNumber { get; }

        public int GetEarnedStarsAmount();
    }
}