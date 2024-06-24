using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zparta.Levels.BonusLevel;
using Zparta.Services;

namespace Zparta.Windows.BonusGame
{
    public class BonusGameWindow_Old : AbstractWindow
    {
        [SerializeField] private Slider chargeSlider;
        [SerializeField] private Text totalScoreText;
        [SerializeField] private Button continueButton;
        [SerializeField] private Animation anim;

        private IBonusLevelWindowController _bonusLevel;
        
        [Inject]
        public void Construct(IBonusLevelWindowController bonusLevel)
        {
            _bonusLevel = bonusLevel;

            _bonusLevel.OnChargeChanged += ChangePunchCharge;
            _bonusLevel.OnResultReceived += DisplayResult;
            continueButton.onClick.AddListener(ContinueClickHandler);
        }

        
        /*public override void Show()
        {
            base.Show();
            
            ShowChargeBar();
        }*/


        private void UpdateScoreText(int value) =>
            totalScoreText.text = value.ToString();

        private void ShowChargeBar()
        {
            chargeSlider.gameObject.SetActive(true);
            chargeSlider.value = 0;
        }

        private void HideChargeBar() => 
            chargeSlider.gameObject.SetActive(false);


        private void ChangePunchCharge(float value)
        {
            chargeSlider.value = value;
            if(value >= 1) 
                HideChargeBar();
        }

        private void DisplayResult(float bonusIndex)
        {
            continueButton.gameObject.SetActive(true);
            //TODO Отобразить результаты.
        }


        private void ContinueClickHandler()
        {
            _bonusLevel.FinishBonusLevel();
            continueButton.gameObject.SetActive(false);
        }
    }
}