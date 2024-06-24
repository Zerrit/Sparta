using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zparta.Windows.Levels
{
    public class LevelButtonView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnClick;
        
        [SerializeField] private Text levelText;
        [SerializeField] private Image levelImage;

        private int _levelId;

        
        public void Initialize(int id)
        {
            _levelId = id;
            levelText.text = (_levelId + 1).ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_levelId);
        }
    }
}