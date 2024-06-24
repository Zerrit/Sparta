using System.Collections;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusLevelExplosionObject : MonoBehaviour
    {
        [SerializeField] Renderer m_objectRenderer;
        [SerializeField] GameObject m_vfx;
        [SerializeField] Vector2 m_minMaxExplosionDelay = new Vector2 (0, 0.5f);

        Vector3 _startPosition;
        Quaternion _startRotation;

        void Awake()
        {
            m_objectRenderer.enabled = true;
            m_vfx.SetActive(false);

            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        IEnumerator ExplosionDelay()
        {
            float delayTime = Random.Range(m_minMaxExplosionDelay.x, m_minMaxExplosionDelay.y);

            yield return new WaitForSeconds(delayTime);

            m_objectRenderer.enabled = false;
            m_vfx.SetActive(true);
        }

        public void ResetObject()
        {
            m_objectRenderer.enabled = true;
            m_vfx.SetActive(false);

            if (_startPosition == Vector3.zero)
                return;

            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }

        public void Explode()
        {
            StartCoroutine(ExplosionDelay());
        }
    }
}