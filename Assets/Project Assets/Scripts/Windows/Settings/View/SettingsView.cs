using IGM.Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zparta.Services;
using Zparta.Windows.Settings.Controllers;

namespace Zparta.Windows.Settings.View
{
    public class SettingsView : AbstractWindow
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Toggle audioToggle;
        [SerializeField] private Toggle vibroToggle;
        [SerializeField] private Button languageSwitch;

        [SerializeField] private Sprite rusIcon;
        [SerializeField] private Sprite engIcon;

        private ISettingsController _controller;
        
        
        [Inject]
        public void Construct(ISettingsController controller)
        {
            _controller = controller;
            
            closeButton.onClick.AddListener(_controller.Close);
            languageSwitch.onClick.AddListener(_controller.SwitchLanguage);
            //TODO настройки звука и вибрации.
        }

        public override void Show()
        {
            base.Show();

            _controller.OnLangIdChanged += UpdateLangIcon;
        }

        public override void Hide()
        {
            base.Hide();
            
            _controller.OnLangIdChanged -= UpdateLangIcon;
        }

        
        
        private void OnEnable()
        {
            languageSwitch.image.sprite = (LocalizationManager.Instance.CurrentLangIndex == 0) ? rusIcon : engIcon;
        }

        private void UpdateLangIcon(int landId)
        {
            languageSwitch.image.sprite = (landId == 0) ? rusIcon : engIcon;
        }
    }
}