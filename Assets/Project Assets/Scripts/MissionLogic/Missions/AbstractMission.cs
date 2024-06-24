namespace Zparta.MissionLogic.Missions
{
    public abstract class AbstractMission
    {
        public bool IsCompleted { get; set; }

        public MissionData Data => _data;
        
        protected MissionData _data;

        public void SetQuestData(MissionData data)
        {
            _data = data;
        }

        
        public abstract void StartTracking();
    }
}