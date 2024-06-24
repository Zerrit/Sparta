using System;
using UnityEngine;

namespace Zparta.Levels.PickableObjects
{
    public class Coin : MonoBehaviour
    {
        public event Action OnPicked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                gameObject.SetActive(false);
                OnPicked?.Invoke();
            }
        }
    }
}