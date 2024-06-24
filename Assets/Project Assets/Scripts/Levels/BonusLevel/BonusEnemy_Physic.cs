using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusEnemy_Physic : BonusEnemy
    {
        public event Action<float> OnStoppedWithScore;
        public override event Action OnStopped;

        [SerializeField] private Rigidbody _selfRigidbody;
        [SerializeField] private Transform _selfTransfrom;
        [SerializeField] private Animator _animator;

        private List<Rigidbody> _rigidbodies;
        private float _targetDistance = 22f;

        private void Awake()
        {
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

            DisableRagdoll();
        }

        [ContextMenu("Включить регдол")]
        private void EnableRagdoll()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = false;
            }

            _animator.enabled = false;
        }

        [ContextMenu("Выключить регдол")]
        private void DisableRagdoll()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = true;
            }

            _animator.enabled = true;
        }


        public override void ResetObject()
        {
            DisableRagdoll();
        }

        public override void Punch(float force)
        {
            EnableRagdoll();
            StartCoroutine(Push((int)force));
        }

        public override void Explode()
        {
            gameObject.SetActive(false);
        }


        private IEnumerator Push(int force)
        {
            _selfRigidbody.AddForce(Vector3.forward * force * 3f, ForceMode.Impulse);

            yield return null;

            while (_selfRigidbody.velocity.sqrMagnitude > 0.05f)
            {
                yield return null;
            }

            StopRagdoll();

            OnStoppedWithScore?.Invoke((transform.position.z / 3) * 0.1f + 1);
            Debug.LogWarning((transform.position.z / 3) * 0.1f + 1);
        }

        private void StopRagdoll()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}