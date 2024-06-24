using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.Configs.Skins;
using Zparta.Configs.Weapons;
using Zparta.Levels.Model;
using Zparta.PlayerLogic.Model;
using Zparta.WalletLogic;
using Zparta.Windows.Customize;
using Object = UnityEngine.Object;

namespace Zparta.Customization
{
    public interface ICustomizationController
    {
        event Action OnEnded;
        
        void Show();
        void Hide();
    }

    public class CustomizationController : ICustomizationController
    {
        public event Action OnEnded;

        private readonly ICustomizationHandler _customizationHandler;
        private readonly CustomizationWindow _window;
        private readonly IPlayerModel _playerModel;
        private readonly IReadOnlyLevelModel _levelModel;
        private readonly AllSkinsConfig _skinsConfig;
        private readonly AllWeaponsConfig _weaponsConfig;
        private readonly ISpendable _wallet;

        private List<CustomizationButtonView> _skinViews;
        private List<CustomizationButtonView> _weaponViews;

        private int _selectedSkinNumber;
        private int _selectedWeaponNumber;

        public CustomizationController(ICustomizationHandler customizationHandler, CustomizationWindow window, 
            IPlayerModel playerModel, IReadOnlyLevelModel levelModel, AllSkinsConfig skinsConfig, AllWeaponsConfig weaponsConfig, ISpendable wallet)
        {
            _customizationHandler = customizationHandler;
            _window = window;
            _playerModel = playerModel;
            _levelModel = levelModel;
            _skinsConfig = skinsConfig;
            _weaponsConfig = weaponsConfig;
            _wallet = wallet;

            _skinViews = new();
            _weaponViews = new();
        }


        public void Show()
        {
            _customizationHandler.Enable();
            _playerModel.PlacePlayer(_customizationHandler.PlayerPosition);
            
            FillSkinsAssortment();
            FillWeaponsAssortment();
            _window.HideControlElements();
            
            _window.BuyButton.OnClick += TryBuySkin;
            _window.HomeButton.onClick.AddListener(EndCustomization);
                
            _window.Show();
        }

        public void Hide()
        {
            _window.HomeButton.onClick.RemoveListener(EndCustomization);
            
            _customizationHandler.Disable();
            
            _window.Hide();
        }

        private void FillSkinsAssortment()
        {            
            foreach (var view in _skinViews)
            {
                Object.Destroy(view.gameObject);
            }
            _skinViews.Clear();
            
            int skinAmount = _skinsConfig.Skins.Length;

            for (int i = 0; i < skinAmount; i++)
            {
                var view = Object.Instantiate(_window.CustomizationButtonViewPrefab, _window.SkinsContainer);
                _skinViews.Add(view);
                
                view.ProductImage.sprite = _skinsConfig.Skins[i].Icon;
                var isAvailable = _playerModel.AvailableSkins.Contains(_skinsConfig.Skins[i].Id);
                view.SwitchAvailable(isAvailable);
                
                if (_skinsConfig.Skins[i].Id == _playerModel.SelectedSkinId)
                {
                    view.SelectedImage.enabled = true;
                }

                view.ProductId = i;
                view.OnClicked += ShowSkin;
            }
        }

        private void FillWeaponsAssortment()
        {
            foreach (var view in _weaponViews)
            {
                Object.Destroy(view.gameObject);
            }
            _weaponViews.Clear();
            
            int weaponsAmount = _weaponsConfig.Weapons.Length;

            for (int i = 0; i < weaponsAmount; i++)
            {
                var view = Object.Instantiate(_window.CustomizationButtonViewPrefab, _window.WeaponsContainer);
                _weaponViews.Add(view);
                
                view.ProductImage.sprite = _weaponsConfig.Weapons[i].Icon;
                var isAvailable = _playerModel.AvailableWeapons.Contains(_weaponsConfig.Weapons[i].Id);
                view.SwitchAvailable(isAvailable);

                view.ProductId = i;
                view.OnClicked += ShowWeapon;
            }
        }

        private void ShowSkin(int number)
        {
            var config = _skinsConfig.Skins[number];
            
            _playerModel.TrySkin(config.Id);

            if (_playerModel.AvailableSkins.Contains(config.Id))
            {
                _window.RequirementPanel.gameObject.SetActive(false);
                _window.BuyButton.gameObject.SetActive(false);

                if (_playerModel.SelectedSkinId == config.Id)
                {
                    
                }
            }
            _window.BuyButton.PriceText.text = config.Price.ToString();
        }

        private void ShowWeapon(int number)
        {
            _playerModel.TryWeapon(_weaponsConfig.Weapons[number].Id);
            var config = _weaponsConfig.Weapons[number];

            if (config.LevelRequirement >= _levelModel.CurrentLevel)
            {
                _window.SelectButton.gameObject.SetActive(false);
                _window.RequirementPanel.SetActive(true);
                _window.RequirementValue.text = config.LevelRequirement.ToString();
                return;
            }
            
            _window.RequirementPanel.SetActive(false);
            _window.SelectButton.gameObject.SetActive(true);

            if (_playerModel.SelectedWeaponId == config.Id)
            {
                _window.SelectButton.SelectionText.text = "Selected";
            }
            else
            {
                _window.SelectButton.SelectionText.text = "Select";
                //_window.SelectionButton.OnClicked +=
            }
        }

        private void TryBuySkin()
        {
            
        }
        
        private void TryBuyWeapon()
        {
            
        }

        private void SelectSkin()
        {
            
        }
        
        private void SelectWeapon()
        {
            
        }

        private void EndCustomization()
        {
            OnEnded?.Invoke();
        }
    }
}