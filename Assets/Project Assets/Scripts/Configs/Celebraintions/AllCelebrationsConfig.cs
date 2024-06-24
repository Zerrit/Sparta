using UnityEngine;

namespace Zparta.Configs.Celebraintions
{
    [CreateAssetMenu(fileName = "AllCelebration", menuName = "Configs/Celebrations/All Celebration")]
    public class AllCelebrationsConfig : ScriptableObject
    {
        [field:SerializeField] public CelebrationConfig[] Celebrations { get; private set; }
    }
}