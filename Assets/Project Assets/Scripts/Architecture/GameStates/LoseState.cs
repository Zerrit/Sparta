using Zparta.Windows.Lose.Controller;

namespace Zparta.Architecture.GameStates
{
    public class LoseState : AbstractGameState
    {
        
        private readonly ILoseController _loseController;
        private readonly GameStateMachine _stateMachine;


        public LoseState(GameStateMachine stateMachine, ILoseController loseController)
        {
            _stateMachine = stateMachine;
            _loseController = loseController;
        }

        public override void Enter()
        {
            base.Enter();

            _loseController.Start();
            _loseController.OnRestart += ToMenu;
            _loseController.OnResurrected += ToGameplay;
        }

        public override void Exit()
        {
            base.Exit();
            
            _loseController.OnRestart -= ToMenu;
            _loseController.OnResurrected -= ToGameplay;
        }


        private void ToMenu() => _stateMachine.ChangeState<MenuState>();

        private void ToGameplay() => _stateMachine.ChangeState<GameplayState>();
    }
}