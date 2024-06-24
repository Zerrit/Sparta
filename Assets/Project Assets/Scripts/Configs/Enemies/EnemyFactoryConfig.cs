using UnityEngine;
using Zparta.EnemiesLogic;

namespace Zparta.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyFactoryConfig", menuName = "Configs/Enemies/EnemyFactoryConfig")]
    public class EnemyFactoryConfig : ScriptableObject
    {
        [field:SerializeField] public EnemyConfig[] Enemies { get; private set; }
    }
}