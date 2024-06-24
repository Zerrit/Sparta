using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zparta.Windows
{
    public class SelectButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;
        
        [field:SerializeField] public Image ButtonBackground { get; private set; }
        [field:SerializeField] public TextMeshProUGUI SelectionText { get; private set; }

        [field:SerializeField] public Color LockColor { get; private set; }
        [field:SerializeField] public Color UnlockColor { get; private set; }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}