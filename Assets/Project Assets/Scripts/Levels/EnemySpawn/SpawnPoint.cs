using UnityEngine;

namespace Zparta.Levels.EnemySpawn
{
    public class SpawnPoint : MonoBehaviour
    {
        public string enemyId;
        public Vector3 Position => transform.position;
    }
}