using UnityEngine;
using Zparta.Factories.Enemies;
using Zparta.Template;

namespace Zparta.EnemiesLogic
{
    public class EnemyPooler : ObjectPooler<Enemy>
    {
        private readonly string _enemyId;
        private readonly IEnemyFactory _enemyFactory;
        
        public EnemyPooler(string enemyId, IEnemyFactory enemyFactory, Transform container, bool isAutoExpend = true) 
            : base(container, isAutoExpend)
        {
            _enemyId = enemyId;
            _enemyFactory = enemyFactory;
        }


        public override Enemy CreateObject(bool isActiveByDefault = false)
        {
            Enemy instance = _enemyFactory.Create(_enemyId, container);
            instance.gameObject.SetActive(isActiveByDefault);
            pool.Add(instance);
            return instance;
        }
    }
}