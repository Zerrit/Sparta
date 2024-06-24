using UnityEngine;

namespace Zparta.EnemiesLogic
{
    /// <summary>
    /// Абстракция для спавнера врагов.
    /// </summary>
    public interface IEnemySpawner
    {
        /// <summary>
        /// Спавнит врага в указанном месте.
        /// </summary>
        public Enemy Spawn(string enemyId, Vector3 position);

        /// <summary>
        /// Спавнит врага в указанном месте и с указанным поворотом.
        /// </summary>
        public Enemy Spawn(string enemyId, Vector3 position, Quaternion rotation);
    }
}