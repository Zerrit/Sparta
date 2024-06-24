using System;
using Zparta.Customization;
using Zparta.MissionLogic;
using Zparta.PlayerLogic.Model;
using Zparta.Windows.Levels;
using Zparta.Windows.MainMenu.Window;
using Zparta.Windows.Settings.View;

namespace Zparta.Windows.MainMenu.Controller
{
    public class MenuController : IMenuController
    {
        public event Action OnStartButtonClicked;
        public event Action OnCustomizationClicked;

        private readonly MenuWindow _menuWindow;
        private readonly SettingsView _settingsView;
        private readonly LevelSelectWindow _levelsWindow;
        private readonly IMissionController _missionController;
        private readonly IPlayerModel _playerModel;
        private readonly ICustomizationController _customizationController;

        public MenuController(MenuWindow menuWindow, SettingsView settingsView, LevelSelectWindow levelsWindow, 
            IMissionController missionController, IPlayerModel playerModel, ICustomizationController customizationController)
        {
            _menuWindow = menuWindow;
            _settingsView = settingsView;
            _levelsWindow = levelsWindow;
            _missionController = missionController;
            _playerModel = playerModel;
            _customizationController = customizationController;
        }

        public void Enable()
        {
            _menuWindow.Show();
            
            UpdateUpgradeButtons();
            
            _menuWindow.BackgroundButton.onClick.AddListener(StartLevel);
            _menuWindow.SettingsButton.onClick.AddListener(OpenSettingsWindow);
            
            _menuWindow.SizeUpgrade.Button.onClick.AddListener(TryUpgradeWeaponSize);
            _menuWindow.PowerUpgrade.Button.onClick.AddListener(TryUpgradeWeaponPower);
            
            _menuWindow.LevelsButton.onClick.AddListener(OpenLevelSelection);
            _menuWindow.CustomizationButton.onClick.AddListener(OpenCustomizationWindow);
            _menuWindow.CelebrationButton.onClick.AddListener(OpenLevelSelection);
            _menuWindow.QuestsButton.onClick.AddListener(OpenQuiestsWindow);


        }

        public void Disable()
        {
            _menuWindow.BackgroundButton.onClick.RemoveListener(StartLevel);
            _menuWindow.SettingsButton.onClick.RemoveListener(OpenSettingsWindow);
            
            _menuWindow.SizeUpgrade.Button.onClick.RemoveListener(TryUpgradeWeaponSize);
            _menuWindow.PowerUpgrade.Button.onClick.RemoveListener(TryUpgradeWeaponPower);
            
            _menuWindow.LevelsButton.onClick.RemoveListener(OpenLevelSelection);
            _menuWindow.CustomizationButton.onClick.RemoveListener(OpenCustomizationWindow);
            _menuWindow.CelebrationButton.onClick.RemoveListener(OpenLevelSelection);
            _menuWindow.QuestsButton.onClick.RemoveListener(OpenQuiestsWindow);

            _menuWindow.Hide();
        }

        private void UpdateUpgradeButtons()
        {
            _menuWindow.SizeUpgrade.UpgradeLevel.text = _playerModel.WeaponSizeUpgradeLevel.ToString();
            _menuWindow.PowerUpgrade.UpgradeLevel.text = _playerModel.WeaponPowerUpgradeLevel.ToString();
            
            _menuWindow.SizeUpgrade.UpgradeCostText.text = _playerModel.WeaponSizeUpgradeCost.ToString();
            _menuWindow.PowerUpgrade.UpgradeCostText.text = _playerModel.WeaponPowerUpgradeCost.ToString();

            _menuWindow.SizeUpgrade.ChangeActive(_playerModel.IsWeaponSizeUpgradeAvailable);
            _menuWindow.PowerUpgrade.ChangeActive(_playerModel.IsWeaponPowerUpgradeAvailable);
        }

        private void StartLevel() 
            => OnStartButtonClicked?.Invoke();
        
        private void OpenLevelSelection()
            => _levelsWindow.Show();

        private void OpenSettingsWindow()
        {
            _settingsView.Show();
        }

        private void OpenQuiestsWindow()
        {
            _missionController.ShowMissionWindow();
        }
        
        private void OpenCustomizationWindow()
        {
            OnCustomizationClicked?.Invoke();
        }
        
        private void OpenCelebrationWindow()
        {
            //_missionController.ShowMissionWindow();
        }

        private void TryUpgradeWeaponSize()
        {
            if (_playerModel.IsWeaponSizeUpgradeAvailable)
            {
                _playerModel.UpgradeWeaponSize();
                UpdateUpgradeButtons();
            }
        }

        private void TryUpgradeWeaponPower()
        {
            if (_playerModel.IsWeaponPowerUpgradeAvailable)
            {
                _playerModel.UpgradeWeaponPower();
                UpdateUpgradeButtons();
            }
        }
    }
}