using System.Collections.Generic;
using UnityEngine;
using Zparta.EnemiesLogic;

namespace Zparta.Factories.Enemies
{
    public interface IEnemyFactory
    {
        IEnumerable<string> GetAllEnemiesId();
        Enemy Create(string enemyId);
        Enemy Create(string enemyId, Transform parent);
        Enemy Create(string enemyId, Vector3 spawnPosition, Quaternion rotation, Transform parent);
    }
}