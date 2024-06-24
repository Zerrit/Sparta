using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Zparta.Levels.BonusLevel
{
    public class BonusEnemy_Mathematic : BonusEnemy
    {
        [Header("References")]        
        
        [SerializeField] Rigidbody _selfRigidbody;
        [SerializeField] Transform m_rotateTransform;
        [SerializeField] Animator _animator;

        [Header("Settings")]
        [SerializeField] float m_speedMultiplayer = 0.5f;
        [SerializeField] float m_maxDistanceForSpeed = 50;
        [SerializeField] AnimationCurve m_speedCurve;
        [SerializeField] AnimationCurve m_verticalMovementCurve;
        [SerializeField] float m_verticalMultiplayer = 3;
        [SerializeField] float m_verticalOffset = -0.5f;

        [Header("Rotation")]
        [SerializeField] Vector2 m_minMaxrotationSpeedMultiplayer = new Vector2(3, 12);
        [SerializeField] AnimationCurve m_rotationSpeedCurve;

        List<Rigidbody> _rigidbodies = new List<Rigidbody>();
        bool _finalRotationActivated;

        public override event Action OnStopped;

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

            _selfRigidbody.isKinematic = true;
            _animator.enabled = false;
        }

        [ContextMenu("Выключить регдол")]
        private void DisableRagdoll()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = true;
            }

            _selfRigidbody.isKinematic = true;
            _animator.enabled = true;
        }


        public override void ResetObject()
        {
            transform.localPosition = Vector3.zero;
            _animator.gameObject.SetActive(true);
            DisableRagdoll();
        }

        public override void Punch(float finalValue)
        {
            _finalRotationActivated = false;

            EnableRagdoll();
            StartCoroutine(Push(finalValue));
        }

        public override void Explode()
        {
            _animator.gameObject.SetActive(false);
        }


        IEnumerator Push(float finalValue)
        {
            while (transform.localPosition.z < finalValue)
            {
                float flyProgress = 1 / (finalValue / transform.localPosition.z);

                float yPos = (m_verticalMovementCurve.Evaluate(flyProgress) * finalValue / 10 * m_verticalMultiplayer) + m_verticalOffset;

                float maxSpeed = finalValue;
                if (maxSpeed > m_maxDistanceForSpeed)
                {
                    maxSpeed = m_maxDistanceForSpeed;
                }

                float speed = m_speedCurve.Evaluate(flyProgress) * maxSpeed * m_speedMultiplayer;
                

                float zPos = transform.localPosition.z + speed * Time.deltaTime;
                transform.localPosition = new Vector3(0, yPos, zPos);

                Rotate(flyProgress, finalValue);
                yield return null;
            }

            StopRagdoll();
            OnStopped?.Invoke();
        }

        void Rotate(float flyProgress, float finalValue)
        {
            float randomSpeed = UnityEngine.Random.Range(m_minMaxrotationSpeedMultiplayer.x, m_minMaxrotationSpeedMultiplayer.y);
            float speed = m_rotationSpeedCurve.Evaluate(flyProgress) * randomSpeed;

            if(finalValue < 10)
            {
                speed = -3;
            }

            if (flyProgress < 0.8f)
            {
                m_rotateTransform.Rotate(new Vector3(1, 1, 0) * speed);
            }
            else
            {
                if (_finalRotationActivated)
                    return;

                _finalRotationActivated = true;

                float x = UnityEngine.Random.Range(75, 105);
                float y = UnityEngine.Random.Range(0, 359);
                float z = UnityEngine.Random.Range(75, 105);

                m_rotateTransform.DORotate(new Vector3(x, y, z), 1);
            }
        }

        void StopRagdoll()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}