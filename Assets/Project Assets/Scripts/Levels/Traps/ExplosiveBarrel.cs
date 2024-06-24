using System.Collections;
using UnityEngine;
using Zparta.Interfaces;

namespace Zparta.Levels.Traps
{
    public class ExplosiveBarrel : AbstractInteraction
    {
        [SerializeField] private float _pushForce;
        [SerializeField] private float _expodeTime;
        [SerializeField] private float _explodeRadius;
        
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private ParticleSystem _fx;
    

        public override void Activate()
        {
            StartCoroutine(Explode());
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IPushable enemy))
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                pushDirection.y = 0;
                enemy.Push(pushDirection * _pushForce);
            }
        }

        private IEnumerator Explode()
        {
            float timer = 0;
            _fx.Play();
            
            while (timer < _expodeTime)
            {
                _collider.radius = _explodeRadius * (timer / _expodeTime);
                
                timer += Time.deltaTime;
                yield return null;
            }
            
            _collider.radius = 0;
            gameObject.SetActive(false);
        }
    }
}