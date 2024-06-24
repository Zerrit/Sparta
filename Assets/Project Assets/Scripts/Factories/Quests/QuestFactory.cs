using System;
using Zenject;
using Zparta.MissionLogic.Missions;

namespace Zparta.Factories.Quests
{
    public class QuestFactory : IQuestFactory
    {
        private readonly DiContainer _container;
        
        public QuestFactory(DiContainer container)
        {
            _container = container;
        }

        public AbstractMission Create(string questId)
        {
            switch (questId)
            {
                case "Gold":
                {
                    return _container.Instantiate<GoldMission>();
                }
                case "Push":
                {
                    return _container.Instantiate<PushMission>();
                }
                case "Combo":
                {
                    return _container.Instantiate<ComboMission>();
                }
            }

            throw new Exception($"Не найдено конфига для квеста с id {questId}");
        }
    }
}