using UnityEngine;
using Zparta.Interfaces;

namespace Zparta.Levels.Traps
{
    public class SpringTrap : BaseTrap
    {
        [SerializeField] private int pushForce;
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IPushable enemy))
            {
                Vector3 pushDirection = new Vector3(1, 1, 0).normalized;
                enemy.Push(pushDirection * pushForce);
            }
        }
    }
}