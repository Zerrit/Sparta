using System;

namespace Zparta.Windows.Settings.Controllers
{
    public interface ISettingsController
    {
        public event Action OnSettingsClosed;
        public event Action<int> OnLangIdChanged;

        public void Open();
        public void Close();
        public void SwitchLanguage();
    }
}