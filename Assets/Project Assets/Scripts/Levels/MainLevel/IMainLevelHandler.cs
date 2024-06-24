using System;

namespace Zparta.Levels.MainLevel
{
    public interface IMainLevelHandler
    {
        public event Action OnMainLevelComplete;

        void InitializeLevel();
        
        void InitializeLevel(int levelId);

        void InitializeCurrentStage();

        void ActivateEnamies();

        void RemoveLevel();
    }
}