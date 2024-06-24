using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.Factories.Enemies;

namespace Zparta.EnemiesLogic
{
    public class EnemySpawner : IEnemySpawner
    {
        private Transform _container;
        
        private readonly Dictionary<string, EnemyPooler> _enemyPoollers = new();
        private readonly IEnemyFactory _factory;
        
        
        public EnemySpawner(IEnemyFactory factory)
        {
            _factory = factory;
            _container = new GameObject("Enemy Container").transform;

            foreach (string enemyId in _factory.GetAllEnemiesId())
            {
                EnemyPooler pooler = new EnemyPooler(enemyId, _factory, _container, true); 
                pooler.CreatePool(20);
                _enemyPoollers[enemyId] = pooler;
            }
        }

        
        
        public Enemy Spawn(string enemyId, Vector3 position)
        {
            if (_enemyPoollers.ContainsKey(enemyId))
            {
                var enemy = _enemyPoollers[enemyId].GetFreeElement();
                enemy.transform.SetPositionAndRotation(position, Quaternion.Euler(0,180,0));
                return enemy;
            }

            throw new Exception($"Не найден префаб врага с указанным id: {enemyId}");
        }
        
        public Enemy Spawn(string enemyId, Vector3 position, Quaternion rotation)
        {
            if (_enemyPoollers.ContainsKey(enemyId))
            {
                var enemy = _enemyPoollers[enemyId].GetFreeElement();
                enemy.transform.SetPositionAndRotation(position, rotation);
                return enemy;
            }

            throw new Exception($"Не найден префаб врага с указанным id: {enemyId}");
        }
    }
}