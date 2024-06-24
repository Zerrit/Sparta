using UnityEngine;
using Zparta.EnemiesLogic;
using Zparta.Interfaces;

namespace Zparta.Levels.Stages
{
    public class WaveStage : AbstractStage
    {
        [SerializeField] private int waveCount;

        private int _currentWave = 1;


        public override void Activate()
        {
            base.Activate();
            finishBridge.ShowBarrier();
        }
        
        protected override void ReduceRemainingEnemies(IEnemy unit)
        {
            base.ReduceRemainingEnemies(unit);
            
            if (_remainingEnemies != 0) return;
            
            if (_currentWave < waveCount)
            {
                _currentWave++;
                SpawnEnemies();
                ActivateEnemies();
            }
            else finishBridge.HideBarrier();
        }

        private void Reset()
        {
            startBridge.ShowBarrier();
            finishBridge.ShowBarrier();
        }
    }
}