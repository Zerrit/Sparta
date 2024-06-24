using Zenject;
using Zparta.Architecture.GameStates;

namespace Zparta.Architecture.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RegisterGameStateMachine();

            RegisterGameStates();
        }
        
        //-----------------------------------------------------------------------------------------------------//
        
        private void RegisterGameStateMachine() 
            => Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        
        private void RegisterGameStates()
        {
            Container.Bind<MenuState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<LoseState>().AsSingle();
            Container.Bind<VictoryState>().AsSingle();
            Container.Bind<CustomizationState>().AsSingle();
            Container.Bind<BonusGameState>().AsSingle();
        }
    }
}