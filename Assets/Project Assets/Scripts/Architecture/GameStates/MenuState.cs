using Zparta.Levels.MainLevel;
using Zparta.PlayerLogic.Model;
using Zparta.Windows.MainMenu.Controller;

namespace Zparta.Architecture.GameStates
{
    public class MenuState : AbstractGameState
    {
        private readonly IMenuController _menuController;
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerModel _playerModel;
        private readonly IMainLevelHandler _levelHandler;


        public MenuState(GameStateMachine stateMachine, IPlayerModel playerModel,  IMainLevelHandler levelHandler, IMenuController menuController)
        {
            _stateMachine = stateMachine;
            _playerModel = playerModel;
            _levelHandler = levelHandler;
            _menuController = menuController;
        }

        public override void Enter()
        {
            base.Enter();
            
            _levelHandler.InitializeLevel();
            _playerModel.ChangePlayerMode(PlayerMode.Default);
            _menuController.Enable();
            
            _menuController.OnStartButtonClicked += ToGameplayState;
            _menuController.OnCustomizationClicked += ToCustomizationState;
        }

        
        public override void Exit()
        {
            base.Exit();
            
            _menuController.OnStartButtonClicked -= ToGameplayState;
            _menuController.OnCustomizationClicked -= ToCustomizationState;
            
            _menuController.Disable();
        }

        private void ToCustomizationState()
        {
            _stateMachine.ChangeState<CustomizationState>();
        }
        
        /*private void ToGameplayState()
        {
            _stateMachine.ChangeState<GameplayState>();
        }*/
        
        private void ToGameplayState()
        {
            _stateMachine.ChangeState<GameplayState>();
        }
    }
}