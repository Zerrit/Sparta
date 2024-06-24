using Zenject;
using Zparta.Interfaces;

namespace Zparta.Architecture.Installers
{
    public class CoreInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
        }
    }
}