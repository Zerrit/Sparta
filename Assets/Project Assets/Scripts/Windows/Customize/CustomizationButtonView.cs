using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zparta.Windows.Customize
{
    public class CustomizationButtonView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnClicked;
        
        [field:SerializeField] public Image BackgroundImage { get; private set; }
        [field:SerializeField] public Image ProductImage { get; private set; }
        [field:SerializeField] public Image LockImage { get; private set; }
        [field:SerializeField] public Image SelectedImage { get; private set; }

        [field:SerializeField] public Color LockColor { get; private set; }
        [field:SerializeField] public Color UnlockColor { get; private set; }

        public int ProductId { get; set; }

        public void SwitchAvailable(bool isAvailable)
        {
            if (isAvailable)
            {
                BackgroundImage.color = UnlockColor;
                LockImage.enabled = false;
                SelectedImage.enabled = false;
            }
            else
            {
                BackgroundImage.color = LockColor;
                LockImage.enabled = true;
                SelectedImage.enabled = false;
            }
        }

        public void SwitchSelection(bool isSelection)
        {
            if (isSelection)
            {
                SelectedImage.enabled = true;
            }
            else
            {
                SelectedImage.enabled = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(ProductId);
        }
    }
}