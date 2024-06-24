using UnityEngine;
using Zenject;
using Zparta.Customization;
using Zparta.EnemiesLogic;
using Zparta.Levels.BonusLevel;
using Zparta.Levels.MainLevel;
using Zparta.Levels.Model;
using Zparta.MissionLogic;
using Zparta.PlayerLogic.Controller;
using Zparta.PlayerLogic.Input;
using Zparta.PlayerLogic.Model;
using Zparta.ScoreLogic;
using Zparta.WalletLogic;
using Zparta.Windows.BonusGame;
using Zparta.Windows.Customize;
using Zparta.Windows.Gameplay;
using Zparta.Windows.Levels;
using Zparta.Windows.Lose.Controller;
using Zparta.Windows.Lose.Window;
using Zparta.Windows.MainMenu.Controller;
using Zparta.Windows.MainMenu.Window;
using Zparta.Windows.Quests;
using Zparta.Windows.Settings.Controllers;
using Zparta.Windows.Settings.View;
using Zparta.Windows.Victory.Controller;
using Zparta.Windows.Victory.Window;
using Zparta.Windows.Wallet;

namespace Zparta.Architecture.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("UI Окна")]
        [SerializeField] private MenuWindow _menuWindow;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private LevelSelectWindow _levelsWindow;
        [SerializeField] private GameplayWindow _gameplayWindow;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private QuestsWindow _questsWindow;
        [SerializeField] private CustomizationWindow _customizationWindow;
        [SerializeField] private WalletWindow _walletWindow;
        [SerializeField] private BonusGameWindow_RandomCards _bonusWindow;
        
        [Header("Other")]
        [SerializeField] private BonusLevelHandler _bonusLevelHandler;
        [SerializeField] private CustomizationHandler _customizationHandler;
        
        [Header("Containers")]
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private Transform _enemyContainer;
        
        public override void InstallBindings()
        {
            RegisterWallet();
            RegisterQuestController();
            RegisterMainLevelHandler();
            RegisterCustomisation();
            RegisterScoreSystem();
            RegisterPlayerInput();
            RegisterPlayerModel();
            RegisterPlayerController();
            RegisterBonusGame();
            RegisterLevelModel();
            RegisterEnemySpawner();
            
            RegisterUI();
            RegisterUIControllers();
        }

        private void RegisterWallet() 
            => Container.BindInterfacesTo<Wallet>().AsSingle();

        private void RegisterQuestController()
            => Container.BindInterfacesAndSelfTo<MissionController>().AsSingle();

        private void RegisterCustomisation()
        {
            Container.Bind<ICustomizationHandler>().FromInstance(_customizationHandler).AsSingle();
            Container.Bind<ICustomizationController>().To<CustomizationController>().AsSingle();
        }
        
        private void RegisterScoreSystem() 
            => Container.BindInterfacesTo<ScoreSystem>().AsSingle();
        
        private void RegisterPlayerInput() 
            => Container.BindInterfacesTo<MouseMoveInput>().AsSingle();

        private void RegisterPlayerModel()
            => Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle();

        private void RegisterPlayerController()
            => Container.Instantiate<PlayerController>();

        private void RegisterBonusGame()
        {
            Container.BindInterfacesAndSelfTo<BonusLevelHandler>().FromInstance(_bonusLevelHandler).AsSingle();
        }

        private void RegisterLevelModel()
        {
            Container.BindInterfacesAndSelfTo<LevelModel>().AsSingle();
        }

        private void RegisterMainLevelHandler()
        {
            Container.BindInterfacesAndSelfTo<MainLevelHandler>().AsSingle().WithArguments(_levelContainer);
        }

        private void RegisterEnemySpawner()
        {
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
        }
        
        private void RegisterUI()
        {
            Container.BindInstance(_menuWindow);
            Container.BindInstance(_settingsView);
            Container.BindInstance(_levelsWindow);
            Container.BindInstance(_gameplayWindow);
            Container.BindInstance(_loseWindow);
            Container.BindInstance(_victoryWindow);
            Container.BindInstance(_questsWindow);
            Container.BindInstance(_customizationWindow);
            Container.BindInstance(_walletWindow);
            Container.BindInstance(_bonusWindow);
        }
        
        private void RegisterUIControllers()
        {
            Container.Bind<ISettingsController>().To<SettingsController>().AsSingle();
            Container.Bind<IMenuController>().To<MenuController>().AsSingle();
            Container.Bind<IGameplayWindowController>().To<GameplayWindowController>().AsSingle();
            Container.Bind<ILoseController>().To<LoseController>().AsSingle();
            Container.Bind<IVictoryController>().To<VictoryController>().AsSingle();
        }
    }
}