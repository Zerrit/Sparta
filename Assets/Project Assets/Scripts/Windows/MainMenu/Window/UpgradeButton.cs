using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zparta.Windows.MainMenu.Window
{
    public class UpgradeButton : MonoBehaviour
    {
        [field:SerializeField] public Button Button { get; private set; }
        [field:SerializeField] public Image Background { get; private set; }
        [field:SerializeField] public TextMeshProUGUI UpgradeLevel { get; private set; }
        [field:SerializeField] public Text UpgradeCostText { get; private set; }
        [field:SerializeField] public Image UnlockIcon { get; private set; }
        [field:SerializeField] public Image LockIcon { get; private set; }
        
        [field:SerializeField] public Color LockColor { get; private set; }
        [field:SerializeField] public Color UnlockColor { get; private set; }

        [SerializeField] private TextMeshProUGUI _levelText;

        public void ChangeActive(bool isUnlock)
        {
            if (isUnlock)
            {
                Background.color = UnlockColor;
                LockIcon.gameObject.SetActive(false);
                UnlockIcon.gameObject.SetActive(true);
            }
            else
            {
                Background.color = LockColor;
                LockIcon.gameObject.SetActive(true);
                UnlockIcon.gameObject.SetActive(false);
            }
        }
    }
}