using System;
using UnityEngine;
using Zenject;
using Zparta.Interfaces;
using Zparta.Levels.Traps;

namespace Zparta.Weapons
{
    public class ChainRoller : AbstractWeapon
    {
        private void OnTriggerEnter(Collider other)
        {
            TryInteract(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TryPushEnemy(other, pushForce);
        }

        private void OnTriggerExit(Collider other)
        {
            TryPushEnemy(other, pushEndForce, true);
        }

        private void TryPushEnemy(Collider triggerCollider, int force, bool resetVelocity = false)
        {
            if (!triggerCollider.CompareTag("Enemy")) return;

            if (triggerCollider.TryGetComponent(out IPushable enemy))
            {
                Vector3 pushDirection = transform.forward;
                pushDirection.y = 0;
                enemy.Push(pushDirection * force, resetVelocity);
            }
        }

        private void TryInteract(Collider triggerCollider)
        {
            if (!triggerCollider.CompareTag("Interaction")) return;
            
            if (triggerCollider.TryGetComponent(out AbstractInteraction interaction))
            {
                interaction.Activate();
            }
        }
    }
}