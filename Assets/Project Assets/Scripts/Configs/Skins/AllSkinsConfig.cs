using UnityEngine;

namespace Zparta.Configs.Skins
{
    [CreateAssetMenu(fileName = "AllSkins", menuName = "Configs/Skins/All Skins")]
    public class AllSkinsConfig : ScriptableObject
    {
        [field:SerializeField] public SkinConfig[] Skins { get; private set; }
    }
}