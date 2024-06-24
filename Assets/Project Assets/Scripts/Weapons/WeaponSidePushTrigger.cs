using UnityEngine;
using Zenject;
using Zparta.Interfaces;
using Zparta.Services;

namespace Zparta.Weapons
{
    public class WeaponSidePushTrigger : MonoBehaviour
    {
        [SerializeField] float pushForce = 10;
        [SerializeField] float maxPushForce = 25;
        [SerializeField] float rotationVelocityKnee = 3;

        float _lastRotationAngleY;
        float _rotationVelocity;

        IUpdateService _updateService;


        [Inject]
        public void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void FixedUpdate()
        {
            CalculateRotationVelocity();
        }
        
        void CalculateRotationVelocity()
        {
            _rotationVelocity = transform.eulerAngles.y - _lastRotationAngleY;
            _lastRotationAngleY = transform.eulerAngles.y;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            if (other.TryGetComponent(out IPushable enemy))
            {
                float rotationVelocity = Mathf.Abs(_rotationVelocity);

                if (rotationVelocity >= rotationVelocityKnee)
                {
                    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                    pushDirection.y = 0;

                    float force = Mathf.Clamp(pushForce * rotationVelocity, 0, maxPushForce);

                    enemy.Push(pushDirection * force);
                }
            }
        }
    }
}