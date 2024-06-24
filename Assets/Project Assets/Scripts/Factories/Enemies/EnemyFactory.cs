using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zparta.Configs.Enemies;
using Zparta.EnemiesLogic;
using Zparta.Services;

namespace Zparta.Factories.Enemies
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly Dictionary<string, EnemyConfig> _enemies = new();
        private readonly EnemyFactoryConfig _config;
        
        public EnemyFactory(DiContainer container, EnemyFactoryConfig config)
        {
            _container = container;
            _config = config;

            foreach (EnemyConfig enemy in _config.Enemies)
            {
                _enemies.Add(enemy.Id, enemy);
            }
        }

        public IEnumerable<string> GetAllEnemiesId() 
            =>_enemies.Keys;

        public Enemy Create(string enemyId)
        {
            if (_enemies.ContainsKey(enemyId))
            {
                return _container.InstantiatePrefabForComponent<Enemy>(_enemies[enemyId].Prefab);
            }
            
            throw new Exception($"Не удалось создать врага с Id: {enemyId}");
        }
        
        public Enemy Create(string enemyId, Transform parent)
        {
            if (_enemies.ContainsKey(enemyId))
            {
                return _container.InstantiatePrefabForComponent<Enemy>(_enemies[enemyId].Prefab, parent);
            }
            
            throw new Exception($"Не удалось создать врага с Id: {enemyId}");
        }
        
        public Enemy Create(string enemyId, Vector3 spawnPosition, Quaternion rotation, Transform parent)
        {
            if (_enemies.ContainsKey(enemyId))
            {
                return _container.InstantiatePrefabForComponent<Enemy>(_enemies[enemyId].Prefab, spawnPosition, rotation, parent);
            }
            
            throw new Exception($"Не удалось создать врага с Id: {enemyId}");
        }
    }
}