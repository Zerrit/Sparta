using UnityEngine;
using Zenject;
using Zparta.Pathfinding;
using Zparta.Services;
using Zparta.Services.PersistentData;
using Zparta.Services.UpdateService;

namespace Zparta.Architecture.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private UpdateService _updateService;
        [SerializeField] private PathfinderSystem _aStarSystem;
        
        public override void InstallBindings()
        {
            RegisterPersistentDataService();
            RegisterUpdateService();
            RegisterAStarSystem();
        }
        
        private void RegisterPersistentDataService()
        {
            Container.Bind<IProgressProvider>().To<PersistentProgressService>().AsSingle();
        }
        
        private void RegisterUpdateService()
        {
            Container.Bind<IUpdateService>().FromInstance(_updateService);
        }
        
        private void RegisterAStarSystem()
        {
            Container.Bind<IPathfinderSystem>().FromInstance(_aStarSystem).AsSingle();
        }
    }
}