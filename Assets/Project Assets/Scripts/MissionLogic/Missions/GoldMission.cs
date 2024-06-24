using Zparta.Levels.Model;

namespace Zparta.MissionLogic.Missions
{
    public class GoldMission : AbstractMission
    {
        private readonly IReadOnlyLevelModel _levelModel;
        
        
        public GoldMission(IReadOnlyLevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public override void StartTracking()
        {
            _levelModel.OnCoinPicked += UpdateProgress;
        }
        
        private void UpdateProgress()
        {
            _data.CurrentProgress++;
            
            if (_data.CurrentProgress >= _data.RequirmentValue)
            {
                _data.CurrentProgress = _data.RequirmentValue;
                IsCompleted = true;
                
                _levelModel.OnCoinPicked -= UpdateProgress;
            }
        }
    }
}