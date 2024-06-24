using UnityEngine;

namespace Zparta.Configs.Celebraintions
{
    [CreateAssetMenu(fileName = "NewCelebraition", menuName = "Configs/Celebrations/New Celebration")]
    public class CelebrationConfig : ScriptableObject
    {
        [field:SerializeField] public string Id { get; private set; }
        [field:SerializeField] public int Price { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public AnimationClip Animation { get; private set; }
    }
}