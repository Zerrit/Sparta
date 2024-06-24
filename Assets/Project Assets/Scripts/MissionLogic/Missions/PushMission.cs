using Zparta.Levels.Model;

namespace Zparta.MissionLogic.Missions
{
    public class PushMission : AbstractMission
    {
        private readonly IReadOnlyLevelModel _levelModel;
        
        public PushMission(IReadOnlyLevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public override void StartTracking()
        {
            _levelModel.OnEnemyPushed += UpdateProgress;
        }
        
        private void UpdateProgress()
        {
            _data.CurrentProgress++;
            
            if (_data.CurrentProgress >= _data.RequirmentValue)
            {
                _data.CurrentProgress = _data.RequirmentValue;
                IsCompleted = true;
                
                _levelModel.OnEnemyPushed -= UpdateProgress;
            }
        }
    }
}