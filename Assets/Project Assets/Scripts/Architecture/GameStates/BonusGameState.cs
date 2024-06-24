using Zparta.Levels.BonusLevel;

namespace Zparta.Architecture.GameStates
{
    public class BonusGameState : AbstractGameState
    {
        private readonly IBonusLevelHandler _bonusLevelHandler;
        private readonly GameStateMachine _stateMachine;

        public BonusGameState(GameStateMachine stateMachine, IBonusLevelHandler bonusLevelHandler)
        {
            _stateMachine = stateMachine;
            _bonusLevelHandler = bonusLevelHandler;
        }

        
        public override void Enter()
        {
            base.Enter();

            _bonusLevelHandler.StartBonusLevel();
            _bonusLevelHandler.OnBonusGameFinished += ToNextLevelHandler;
        }

        public override void Exit()
        {
            base.Exit();
            
            _bonusLevelHandler.OnBonusGameFinished -= ToNextLevelHandler;
        }


        private void ToNextLevelHandler() 
            => _stateMachine.ChangeState<MenuState>();
    }
}