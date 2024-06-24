using System.Collections;
using UnityEngine;

namespace IGM.Core
{
    public class SingleUpdate : MonoBehaviour
    {
        #region Singelton
        private static SingleUpdate _instance;
        public static SingleUpdate Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<SingleUpdate>();

                return _instance;
            }
        }
        #endregion

        [SerializeField] float m_optimizedUpdateTime = 0.1f;

        public delegate void updateDelegate();
        public updateDelegate UpdateDelegate;

        public delegate void optimizedUpdateDelegate();
        public optimizedUpdateDelegate OptimizedUpdateDelegate;

        void Start()
        {
            StartCoroutine(OptimizedUpdate());
        }

        void Update()
        {
            UpdateDelegate?.Invoke();
        }

        IEnumerator OptimizedUpdate()
        {
            yield return new WaitForSeconds(m_optimizedUpdateTime);

            OptimizedUpdateDelegate?.Invoke();

            StartCoroutine(OptimizedUpdate());
        }

        public void startCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }

        public void stopCoroutine(IEnumerator enumerator)
        {
            StopCoroutine(enumerator);
        }
    }
}