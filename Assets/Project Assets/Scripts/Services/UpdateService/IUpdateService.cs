using System;
using Zparta.Interfaces;

namespace Zparta.Services
{
    public interface IUpdateService
    {
        public event Action OnTick;
        
        void AddToList(IUpdatable updatable);
        void RemoveFromList(IUpdatable updatable);
    }
}