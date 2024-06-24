using Zparta.MissionLogic.Missions;

namespace Zparta.Factories.Quests
{
    public interface IQuestFactory
    {
        public AbstractMission Create(string questId);
    }
}