using Zparta.Levels.MainLevel;
using Zparta.PlayerLogic.Model;
using Zparta.Windows.Victory.Controller;

namespace Zparta.Architecture.GameStates
{
    public class VictoryState : AbstractGameState
    {
        private readonly IVictoryController _victoryController;
        private readonly IMainLevelHandler _mainLevelHandler;
        private readonly IPlayerModel _playerModel;
        private readonly GameStateMachine _stateMachine;
        
        public VictoryState(GameStateMachine gameStateMachine, IVictoryController victoryController, IMainLevelHandler mainLevelHandler, IPlayerModel playerModel)
        {
            _stateMachine = gameStateMachine;
            _victoryController = victoryController;
            _mainLevelHandler = mainLevelHandler;
            _playerModel = playerModel;
        }

        
        public override void Enter()
        {
            base.Enter();
            
            _victoryController.Open();
            _playerModel.ChangePlayerMode(PlayerMode.Celebrating);
            _victoryController.OnContinueClick += ToBonusLevel;
        }

        public override void Exit()
        {
            base.Exit();
            
            _victoryController.OnContinueClick -= ToBonusLevel;
            
            _playerModel.RemovePlayer();
            _mainLevelHandler.RemoveLevel();
        }

        private void ToBonusLevel()
        {
            _stateMachine.ChangeState<BonusGameState>();
        }
    }
}