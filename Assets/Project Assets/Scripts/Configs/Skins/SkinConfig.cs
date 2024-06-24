using UnityEngine;

namespace Zparta.Configs.Skins
{
    [CreateAssetMenu(fileName = "NewSkin", menuName = "Configs/Skins/New Skin")]
    public class SkinConfig : ScriptableObject
    {
        [field:SerializeField] public string Id { get; private set; }
        [field:SerializeField] public int Price { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public GameObject Prefab { get; private set; }
    }
}