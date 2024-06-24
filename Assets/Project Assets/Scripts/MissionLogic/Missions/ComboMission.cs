using Zparta.ScoreLogic;

namespace Zparta.MissionLogic.Missions
{
    public class ComboMission : AbstractMission
    {
        private readonly IScoreListener _scoreListener;
        

        public ComboMission(IScoreListener scoreListener)
        {
            _scoreListener = scoreListener;
        }

        public override void StartTracking()
        {
            _scoreListener.OnComboChange += UpdateProgress;
        }

        private void UpdateProgress(int comboValue)
        {
            if(comboValue != 50) return;    
            
            _data.CurrentProgress++;
            
            if (_data.CurrentProgress >= _data.RequirmentValue)
            {
                _data.CurrentProgress = _data.RequirmentValue;
                
                IsCompleted = true;
                
                _scoreListener.OnComboChange -= UpdateProgress;
            }
        }
    }
}