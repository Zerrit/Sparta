using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Zparta.EnemiesLogic
{
    public class RandomDelayAnimationOnEnable : MonoBehaviour
    {
        [SerializeField] Animator m_animator;
        [SerializeField] string m_stateName;
        [SerializeField] Vector2 m_minMaxEnableDelay = new Vector2(0, 0.5f);
        [SerializeField] UnityEvent m_onEnable;
        [SerializeField] UnityEvent m_onPlayAnim;

        void OnEnable()
        {
            StartCoroutine(Delay());
            m_onEnable?.Invoke();
        }

        IEnumerator Delay()
        {
            float randomDealy = Random.Range(m_minMaxEnableDelay.x, m_minMaxEnableDelay.y);

            yield return new WaitForSeconds(randomDealy);

            m_onPlayAnim?.Invoke();
            m_animator.playbackTime = 0;
            m_animator.Play(m_stateName);
        }
    }
}