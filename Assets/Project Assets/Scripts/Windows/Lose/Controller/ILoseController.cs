using System;

namespace Zparta.Windows.Lose.Controller
{
    public interface ILoseController
    {
        public event Action OnRestart;
        public event Action OnResurrected;

        public void Start();
        public void Close();
    }
}