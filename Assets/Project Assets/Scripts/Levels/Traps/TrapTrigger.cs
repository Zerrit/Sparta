using System;
using UnityEngine;

namespace Zparta.Levels.Traps
{
    public class TrapTrigger : MonoBehaviour
    {
        public event Action OnTriggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                OnTriggered?.Invoke();
        }
    }
}