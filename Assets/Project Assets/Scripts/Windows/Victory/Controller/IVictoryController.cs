using System;

namespace Zparta.Windows.Victory.Controller
{
    public interface IVictoryController
    {
        public event Action OnContinueClick;

        public void Open();
    }
}