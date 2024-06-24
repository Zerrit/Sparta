using System;
using UnityEngine;
using Zparta.PlayerLogic;
using Zparta.PlayerLogic.View;

namespace Zparta.Levels.PickableObjects
{
    public class ChargePowerUp : AbstractPowerUp
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                if (other.transform.root.TryGetComponent(out Player player))
                {
                    player.BoostCharge(1f);
                    HideSelf();
                    Debug.LogWarning("Оружие заряжено на 100%");
                }
            }
        }
    }
}