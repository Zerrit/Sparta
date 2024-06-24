using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zparta.Windows.Customize
{
    public class CustomizationWindow : AbstractWindow
    {
        [field: SerializeField] public CustomizationButtonView CustomizationButtonViewPrefab { get; private set; }

        [field: SerializeField] public Button HomeButton { get; private set; }

        [field: SerializeField] public GameObject RequirementPanel { get; private set; }
        [field: SerializeField] public TextMeshProUGUI RequirementValue { get; private set; }
        [field: SerializeField] public GameObject SelectedLable { get; private set; }
        [field: SerializeField] public SelectButton SelectButton { get; private set; }
        [field: SerializeField] public BuyButton BuyButton { get; private set; }

        [field: SerializeField] public Transform SkinsContainer { get; private set; }
        [field: SerializeField] public Transform WeaponsContainer { get; private set; }

        public void HideControlElements()
        {
            RequirementPanel.SetActive(false);
            SelectButton.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(false);
        }
    }
}