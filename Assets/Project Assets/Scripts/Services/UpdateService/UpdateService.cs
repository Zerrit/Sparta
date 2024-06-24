using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.Interfaces;

namespace Zparta.Services.UpdateService
{
    public class UpdateService : MonoBehaviour, IUpdateService
    {
        public event Action OnTick;
        
        [SerializeField] private float optimizedDelay = .1f;

        private bool _isNeedUpdate;
        
        private float _lastTimeOptimizedUpdate;

        private readonly List<IUpdatable> _optimizedUpdateList = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnTick?.Invoke();
            
            if(!_isNeedUpdate) return;
            
            if (Time.time >= _lastTimeOptimizedUpdate + optimizedDelay)
            {
                for (int i = 0; i < _optimizedUpdateList.Count; i++)
                {
                    _optimizedUpdateList[i].OptimizedUpdate();
                }
                
                _lastTimeOptimizedUpdate = Time.time;
            }
        }

        private void FixedUpdate()
        {
            if(!_isNeedUpdate) return;
            
            for (int i = 0; i < _optimizedUpdateList.Count; i++)
            {
                _optimizedUpdateList[i].PhysicsUpdate();
            }
        }

        public void AddToList(IUpdatable updatable)
        {
            _optimizedUpdateList.Add(updatable);

            if (!_isNeedUpdate) 
                _isNeedUpdate = true;
        }
        
        public void RemoveFromList(IUpdatable updatable)
        {
            _optimizedUpdateList.Remove(updatable);

            if (_optimizedUpdateList.Count == 0)
                _isNeedUpdate = false;
        }
    }
}