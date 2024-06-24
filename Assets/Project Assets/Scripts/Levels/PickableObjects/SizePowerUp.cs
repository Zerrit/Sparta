using UnityEngine;
using Zparta.PlayerLogic;
using Zparta.PlayerLogic.View;

namespace Zparta.Levels.PickableObjects
{
    public class SizePowerUp : AbstractPowerUp
    {
        [SerializeField] private float _upgradeSize;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                if (other.transform.root.TryGetComponent(out Player player))
                {
                    player.BoostSize(_upgradeSize);
                    HideSelf();
                    Debug.Log("Прокачка оружия");
                }
            }
        }
    }
}