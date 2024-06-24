using UnityEngine;

namespace Zparta.Configs.Levels
{
    [CreateAssetMenu(fileName = "LevelFactoryConfig", menuName = "Configs/Levels/LevelFactory")]
    public class LevelFactoryConfig : ScriptableObject
    {
        [field:SerializeField] public LevelConfig[] Levels { get; private set; }
    }
}