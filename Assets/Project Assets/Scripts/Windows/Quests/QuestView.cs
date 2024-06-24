using System;
using IGM.Localization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zparta.Windows.Quests
{
    public class QuestView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnClicked;

        public int MissionId { get; set; }
        
        [field: SerializeField] public GameObject CompletedInfo { get; private set; }
        [field: SerializeField] public GameObject QuestInfo { get; private set; }
        
        [field: SerializeField] public Image Background { get; private set; }
        [field: SerializeField] public LocalizedText_TMP DescriptionLocId { get; private set; }
        [field: SerializeField] public Slider ProgressSlider { get; private set; }
        [field: SerializeField] public Text ProgressText { get; private set; }
        [field: SerializeField] public Image CheckIcon { get; private set; }
        [field: SerializeField] public Text RewardValue { get; private set; }
        
        [field: SerializeField] public Color InProgressColor { get; private set; }
        [field: SerializeField] public Color CompletedColor { get; private set; }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(MissionId);
        }
    }
}