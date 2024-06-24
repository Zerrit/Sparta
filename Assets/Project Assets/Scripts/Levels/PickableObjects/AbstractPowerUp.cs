using System;
using UnityEngine;

namespace Zparta.Levels.PickableObjects
{
    public class AbstractPowerUp : MonoBehaviour
    {
        public event Action OnPicked;

        protected void HideSelf()
        {
            OnPicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}