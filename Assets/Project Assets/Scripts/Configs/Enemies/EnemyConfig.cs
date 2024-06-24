using UnityEngine;
using Zparta.EnemiesLogic;

namespace Zparta.Configs.Enemies
{
    [CreateAssetMenu(fileName = "DefaultEnemyConfig", menuName = "Configs/Enemies/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field:SerializeField] public string Id { get; private set; }
        [field:SerializeField] public Enemy Prefab { get; private set; }
    }
}