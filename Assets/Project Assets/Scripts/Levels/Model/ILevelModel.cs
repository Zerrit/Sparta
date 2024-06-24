namespace Zparta.Levels.Model
{
    public interface ILevelModel
    {
        public int LevelCount { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentStage { get; set; }
        public int StageCount { get; set; }
        
        public int[] StarsTresholds { get; set; }

        void ClearTempData();

        /// <summary>
        /// Обрабатывает количество появившихся на этапе врагов.
        /// </summary>
        void HandleSpawnedEnemies(int amount);

        /// <summary>
        /// Обрабатывает событие о столкнутом враге.
        /// </summary>
        /// <param name="rewardValue"> Размер награды за врага. </param>
        void HandleKilledEnemy(int rewardValue);

        /// <summary>
        /// Обрабатывает событие о зачистке этапа.
        /// </summary>
        void HandleClearedStage();

        /// <summary>
        /// Обрапбатывает подбор монетки.
        /// </summary>
        void HandlePickedCoin();

        /// <summary>
        /// Обрабатывает подбор поверапа.
        /// </summary>
        void HandlePickedPowerUp();
    }
}