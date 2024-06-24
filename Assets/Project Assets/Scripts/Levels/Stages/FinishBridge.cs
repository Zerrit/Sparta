using System;
using UnityEngine;

namespace Zparta.Levels.Stages
{
    public class FinishBridge : MonoBehaviour
    {
        public event Action OnPlayerFinished;

        [field:SerializeField] public Transform EndPoint { get; private set; }
        [SerializeField] private GameObject barrier;

        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                OnPlayerFinished?.Invoke();
        }
        
        public void ShowBarrier() => barrier.SetActive(true);
        public void HideBarrier() => barrier.SetActive(false);
    }
}