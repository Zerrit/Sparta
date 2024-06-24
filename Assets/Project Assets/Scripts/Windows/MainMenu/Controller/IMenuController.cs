using System;

namespace Zparta.Windows.MainMenu.Controller
{
    public interface IMenuController
    {
        public event Action OnStartButtonClicked;
        public event Action OnCustomizationClicked;

        public void Enable();
        public void Disable();
    }
}