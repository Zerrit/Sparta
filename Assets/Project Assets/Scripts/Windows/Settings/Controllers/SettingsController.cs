using System;
using IGM.Localization;
using Zparta.Services;
using Zparta.Windows.Settings.View;

namespace Zparta.Windows.Settings.Controllers
{
    public class SettingsController : ISettingsController
    {
        public event Action OnSettingsClosed;
        public event Action<int> OnLangIdChanged;
        
        private int _currentLangId;
        
        private readonly SettingsView _settingView;
        
        public SettingsController(SettingsView settingView)
        {
            _settingView = settingView;
        }
        
        
        public void Open()
        {
            _settingView.Show();
        }
        
        public void Close()
        {
            _settingView.Hide();
        }

        public void SwitchLanguage()
        {
            _currentLangId = (_currentLangId == 0) ? 1 : 0;
            LocalizationManager.Instance.SetLanguage(_currentLangId);
            OnLangIdChanged?.Invoke(_currentLangId);
        }
    }
}