using UnityEngine;

namespace Zparta.Configs.Weapons
{
    [CreateAssetMenu(fileName = "AllWeapons", menuName = "Configs/Weapons/All Weapons")]
    public class AllWeaponsConfig : ScriptableObject
    {
        [field:SerializeField] public WeaponConfig[] Weapons { get; private set; }
    }
}