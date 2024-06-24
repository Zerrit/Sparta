using System;
using System.Collections;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusLevelCard : MonoBehaviour
    {
        [SerializeField] BonusInfo m_bonusInfo;
        [SerializeField] Animator m_animator;
        [SerializeField] Vector2 m_minMaxRandomSpawnTime = new Vector2 (0, 0.3f);
        [SerializeField] float m_openAnimationTime = 2;
        [SerializeField] float m_despawnAnimationTime = 1;

        public event Action<BonusLevelCard> EOn_Open;
        public event Action<BonusLevelCard, BonusInfo> EOn_ReciveCardInfo;

        void PlaySpawnAnimation()
        {
            m_animator.playbackTime = 0;
            m_animator.Play("Spawn");
        }

        public void Spawn()
        {
            float randomSpawnTime = UnityEngine.Random.Range(m_minMaxRandomSpawnTime.x, m_minMaxRandomSpawnTime.y);
            Invoke(nameof(PlaySpawnAnimation), randomSpawnTime);
        }

        public void Open()
        {
            m_animator.CrossFade("Open", 0.1f);
            StartCoroutine(OpenDelay());

            EOn_Open?.Invoke(this);
        }
        
        public void Despawn()
        {
            m_animator.CrossFade("Despawn", 0.1f);
            StartCoroutine(DespawnDelay());
        }

        IEnumerator OpenDelay()
        {
            yield return new WaitForSeconds(m_openAnimationTime);
            EOn_ReciveCardInfo?.Invoke(this, m_bonusInfo);
        }

        IEnumerator DespawnDelay()
        {
            yield return new WaitForSeconds(m_despawnAnimationTime);
            Destroy(gameObject);
        }
    }
}