using UnityEngine;
using Zparta.Interfaces;

namespace Zparta.Levels.Traps
{
    public class SideKickTrap : BaseTrap
    {
        [SerializeField] private int pushForce;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IPushable enemy))
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                pushDirection.y = 0;
                enemy.Push(pushDirection * pushForce);
            }
        }
    }
}