using UnityEngine;
using Zparta.Factories.Character;
using Zparta.PlayerLogic.Model;
using Zparta.PlayerLogic.View;

namespace Zparta.PlayerLogic.Controller
{
    public class PlayerController
    {
        private Player _playerInstance;

        private readonly Vector3 _spawnOffset;
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerFactory _playerFactory;

        public PlayerController(IPlayerModel playerModel, IPlayerFactory playerFactory)
        {
            _playerModel = playerModel;
            _playerFactory = playerFactory;

            _playerModel.OnPlayerPlacementRequested += SpawnPlayer;
            _playerModel.OnPlayerRemovalRequested += DespawnPlayer;
            _playerModel.OnPlayerModeChanged += ChangePlayerMode;
            _playerModel.OnChargeIncreased += UpdatePlayerCharge;
            _playerModel.OnWeaponSizeChanged += UpdateWeaponSize;
            _playerModel.OnWeaponPowerChanged += UpdateWeaponPower;
            _playerModel.OnSkinChanged += UpdateSkin;
            _playerModel.OnWeaponChanged += UpdateWeapon;
        }

        public void SpawnPlayer(Vector3 position)
        {
            if (_playerInstance == null)
            {
                CreateInstance(position);
            }
            else
            {
                TranslateInstance(position);
            }
            
            ResetInstance();
        }

        private void CreateInstance(Vector3 position)
        {
            _playerInstance = _playerFactory.Create(position, Quaternion.identity);
            
            _playerInstance.OnDespawned += DespawnPlayer;
            _playerInstance.OnSuperAttackEnabled += ApplySuperAttack;
        }

        private void TranslateInstance(Vector3 position)
        {
            _playerInstance.SelfTransform.gameObject.SetActive(false);
            _playerInstance.Refresh();
            _playerInstance.SelfTransform.position = position;
            _playerInstance.SelfTransform.gameObject.SetActive(true);
            
            _playerInstance.OnDespawned += DespawnPlayer;
            _playerInstance.OnSuperAttackEnabled += ApplySuperAttack;
        }

        private void ResetInstance()
        {
            _playerModel.RegisterInstance(_playerInstance.SelfTransform);
            
            UpdateSkin(_playerModel.SelectedSkinId);
            UpdateWeapon(_playerModel.SelectedWeaponId);
            
            _playerInstance.Refresh();
            _playerInstance.Weapon.SetSize(_playerModel.WeaponAdditionalSize);
            ChangePlayerMode(PlayerMode.Default);
        }

        private void UpdateSkin(string id)
        {
            var amount = _playerInstance.SkinHandler.childCount;

            if (amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    Object.Destroy(_playerInstance.SkinHandler.GetChild(i).gameObject);
                }
            }

            _playerFactory.CreateSkin(id, _playerInstance.SkinHandler);
        }
        
        private void UpdateWeapon(string id)
        {
            var amount = _playerInstance.WeaponHandler.childCount;

            if (amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    Object.Destroy(_playerInstance.WeaponHandler.GetChild(i).gameObject);
                }
            }
            
            _playerInstance.Weapon = _playerFactory.CreateWeapon(id, _playerInstance.WeaponHandler);
        }
        
        private void ChangePlayerMode(PlayerMode mode)
        {
            switch (mode)
            {
                case PlayerMode.Default:
                {
                    _playerInstance.ChangeMode(mode);
                    break;
                }
                case PlayerMode.Celebrating:
                {
                    _playerInstance.ChangeMode(mode);
                    break;
                }
            }   
        }

        private void UpdatePlayerCharge(int value)
        {
            _playerInstance?.UpdateWeaponCharge(value);
        }
        
        private void UpdateWeaponSize(float value)
        {
            _playerInstance?.Weapon.SetSize(value);
        }
        
        private void UpdateWeaponPower(int value)
        {
            _playerInstance?.Weapon.SetPower(value);
        }

        private void ApplySuperAttack()
        {
            _playerModel.SendSuperAttackEvent();
        }
        
        private void DespawnPlayer()
        {
            if (_playerInstance == null)
            {
                Debug.LogWarning("При попытке удаления не был обнаружен существующий инстанс персонажа");
                return;
            }

            _playerInstance.OnDespawned -= DespawnPlayer;
            _playerInstance.OnSuperAttackEnabled -= ApplySuperAttack;
            
            _playerModel.UnregisterInstance();
            _playerInstance.Despawn();
        }
    }
} 