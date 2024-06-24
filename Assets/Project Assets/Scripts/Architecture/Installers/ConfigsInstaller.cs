using UnityEngine;
using Zenject;
using Zparta.Configs;
using Zparta.Configs.Celebraintions;
using Zparta.Configs.Enemies;
using Zparta.Configs.Levels;
using Zparta.Configs.Missions;
using Zparta.Configs.Skins;
using Zparta.Configs.Weapons;

namespace Zparta.Architecture.Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private AllSkinsConfig _skinsConfig;
        [SerializeField] private AllWeaponsConfig _weaponsConfig;
        [SerializeField] private AllCelebrationsConfig _celebrationsConfig;
        [SerializeField] private MissionPoolConfig _missionsConfig;
        [SerializeField] private JoystickFactoryConfig _joystickConfig;
        [SerializeField] private LevelFactoryConfig _levelsConfig;
        [SerializeField] private EnemyFactoryConfig _enemiesConfig;

        
        public override void InstallBindings()
        {
            Container.BindInstance(_skinsConfig);
            Container.BindInstance(_weaponsConfig);
            Container.BindInstance(_celebrationsConfig);
            Container.BindInstance(_missionsConfig);
            Container.BindInstance(_joystickConfig);
            Container.BindInstance(_levelsConfig);
            Container.BindInstance(_enemiesConfig);
        }
    }
}