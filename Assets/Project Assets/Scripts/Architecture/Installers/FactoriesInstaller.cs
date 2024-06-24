using UnityEngine;
using Zenject;
using Zparta.Factories;
using Zparta.Factories.Character;
using Zparta.Factories.Enemies;
using Zparta.Factories.Joysticks;
using Zparta.Factories.Levels;
using Zparta.Factories.Quests;
using Zparta.PlayerLogic.View;

namespace Zparta.Architecture.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;

        public override void InstallBindings()
        {
            RegisterGameStateFactory();
            RegisterJoystickFactory();
            RegisterPlayerFactory();
            RegisterEnemyFactory();
            RegisterLevelFactory();
            RegisterQuestFactory();
        }

        private void RegisterGameStateFactory() 
            => Container.Bind<GameStateFactory>().AsSingle().NonLazy();

        private void RegisterJoystickFactory()
            => Container.Bind<IJoystickFactory>().To<JoystickFactory>().AsSingle().NonLazy();
        
        private void RegisterPlayerFactory() 
            => Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle().WithArguments(_playerPrefab).NonLazy();

        private void RegisterEnemyFactory() 
            => Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().NonLazy();

        private void RegisterLevelFactory()
            => Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle().NonLazy();

        private void RegisterQuestFactory()
            => Container.Bind<IQuestFactory>().To<QuestFactory>().AsSingle().NonLazy();
    }
}
    