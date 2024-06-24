using Zparta.Customization;
using Zparta.Levels.MainLevel;
using Zparta.PlayerLogic.Model;

namespace Zparta.Architecture.GameStates
{
    public class CustomizationState : AbstractGameState
    {
        private readonly IMainLevelHandler _mainLevelHandler;
        private readonly IPlayerModel _playerModel;
        private readonly GameStateMachine _stateMachine;
        private readonly ICustomizationController _customizationController;

        public CustomizationState(GameStateMachine gameStateMachine, ICustomizationController customizationController, IMainLevelHandler mainLevelHandler, IPlayerModel playerModel)
        {
            _stateMachine = gameStateMachine;
            _customizationController = customizationController;
            _mainLevelHandler = mainLevelHandler;
            _playerModel = playerModel;
        }

        
        public override void Enter()
        {
            base.Enter();
            
            _mainLevelHandler.RemoveLevel();
            _customizationController.Show();

            _customizationController.OnEnded += ToMenu;
        }

        public override void Exit()
        {
            base.Exit();
            
            _customizationController.Hide();
            
            _customizationController.OnEnded -= ToMenu;
        }

        private void ToMenu()
        {
            _stateMachine.ChangeState<MenuState>();
        }
    }
}