using Zparta.Levels.MainLevel;
using Zparta.PlayerLogic;
using Zparta.PlayerLogic.Model;
using Zparta.Windows.Gameplay;

namespace Zparta.Architecture.GameStates
{
    public class GameplayState : AbstractGameState
    {
        private readonly IGameplayWindowController _gameplayController;
        private readonly IInputBlocker _inputBlocker;
        private readonly IMainLevelHandler _levelHandler;
        private readonly IPlayerHandler _playerHandler;
        private readonly GameStateMachine _stateMachine;

        public GameplayState(GameStateMachine stateMachine, IInputBlocker inputBlocker, IMainLevelHandler levelHandler, 
            IPlayerHandler playerHandler, IGameplayWindowController gameplayWindowController)
        {
            _stateMachine = stateMachine;
            _inputBlocker = inputBlocker;
            _levelHandler = levelHandler;
            _playerHandler = playerHandler;
            _gameplayController = gameplayWindowController;
        }


        public override void Enter()
        {
            base.Enter();

            _gameplayController.Show();

            _levelHandler.ActivateEnamies();
            _inputBlocker.UnblockInput();

            _gameplayController.OnExit += ToMenuState;
            _playerHandler.OnPlayerLost += ToLoseState;
            _levelHandler.OnMainLevelComplete += ToVictoryState;
        }

        public override void Exit()
        {
            base.Exit();
            
            _gameplayController.Hide();
            _inputBlocker.BlockInput();

            _gameplayController.OnExit -= ToMenuState;
            _playerHandler.OnPlayerLost -= ToLoseState;
            _levelHandler.OnMainLevelComplete -= ToVictoryState;
        }

        private void ToMenuState()
        {
            _stateMachine.ChangeState<MenuState>();
        }
        
        private void ToLoseState()
        {
            _stateMachine.ChangeState<LoseState>();
        }

        private void ToVictoryState()
        {
            _stateMachine.ChangeState<VictoryState>();
        }
    }
}