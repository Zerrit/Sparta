using UnityEngine;
using Zparta.Weapons;

namespace Zparta.Configs.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Configs/Weapons/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [field:SerializeField] public string Id { get; private set; }
        [field:SerializeField] public int LevelRequirement { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public AbstractWeapon Weapons { get; private set; }
    }
}