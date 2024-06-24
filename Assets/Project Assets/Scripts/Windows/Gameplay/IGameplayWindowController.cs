using System;

namespace Zparta.Windows.Gameplay
{
    public interface IGameplayWindowController
    {
        public event Action OnExit;
        
        public void Show();
        public void Hide();
    }
}