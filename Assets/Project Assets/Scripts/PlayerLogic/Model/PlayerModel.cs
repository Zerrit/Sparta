using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.ScoreLogic;
using Zparta.WalletLogic;

namespace Zparta.PlayerLogic.Model
{
    public enum PlayerMode
    {
        Default = 0,
        Celebrating = 1
    }

    public class PlayerModel : IPlayerModel, IPlayerHandler
    {
        public event Action<Vector3> OnPlayerPlacementRequested;
        public event Action OnPlayerRemovalRequested;
        
        public event Action OnSuperAttacked;

        public event Action<PlayerMode> OnPlayerModeChanged;
        public event Action<int> OnChargeIncreased;
        public event Action<float> OnWeaponSizeChanged;
        public event Action<int> OnWeaponPowerChanged;

        public event Action<string> OnSkinChanged;
        public event Action<string> OnWeaponChanged;
        public event Action<string> OnSelebrationChanged;

        public event Action OnPlayerSpawned;
        public event Action OnPlayerLost;

        public bool IsPlayerExist { get; private set; }
        public Transform PlayerTransform { get; private set; }
        
        public string SelectedSkinId { get; private set; }
        public List<string> AvailableSkins { get; private set; }
        
        public string SelectedWeaponId { get; private set; }
        public List<string> AvailableWeapons { get; private set; }
        
        public string SelectedCelebrationId { get; private set; }
        public List<string> AvailableCelebrations { get; private set; }
        
        public int WeaponAdditionalPower { get; private set; }
        public float WeaponAdditionalSize { get; private set; }

        public int WeaponSizeUpgradeLevel { get; private set; }
        public int WeaponSizeUpgradeCost { get; private set; }
        public bool IsWeaponSizeUpgradeAvailable => _wallet.CheckPurchaseAbility(WeaponSizeUpgradeCost);

        public int WeaponPowerUpgradeLevel { get; private set; }
        public int WeaponPowerUpgradeCost { get; private set; }
        public bool IsWeaponPowerUpgradeAvailable => _wallet.CheckPurchaseAbility(WeaponPowerUpgradeCost);

        private readonly ISpendable _wallet;
        private readonly IScoreListener _scoreListener;

        public PlayerModel(ISpendable wallet, IScoreListener scoreListener)
        {
            _wallet = wallet;
            _scoreListener = scoreListener;
            //TRY Load Data or init from config

            AvailableSkins = new ();
            AvailableSkins.Add("Default");
            SelectedSkinId = AvailableSkins[0];
            
            AvailableWeapons = new ();
            AvailableWeapons.Add("Roller");
            SelectedWeaponId = AvailableWeapons[0];
            
            AvailableCelebrations = new ();
            AvailableCelebrations.Add("Default");
            SelectedCelebrationId = AvailableCelebrations[0];
            
            WeaponAdditionalSize = 0f;
            WeaponAdditionalPower = 0;
            
            WeaponSizeUpgradeLevel = 1;
            WeaponPowerUpgradeLevel = 1;
            
            WeaponSizeUpgradeCost = 5;
            WeaponPowerUpgradeCost = 5;

            _scoreListener.OnScoreIncrease += IncreaseWeaponCharge;
        }

        public void PlacePlayer(Vector3 spawnPos)
        {
            OnPlayerPlacementRequested?.Invoke(spawnPos);
        }

        public void RemovePlayer()
        {
            OnPlayerRemovalRequested?.Invoke();
        }

        /// <summary>
        /// Ставит на учёт активный инстанс героя на сцене.
        /// </summary>
        public void RegisterInstance(Transform playerTransform)
        {
            PlayerTransform = playerTransform;
            IsPlayerExist = true;
            OnPlayerSpawned?.Invoke();
        }

        /// <summary>
        /// Снимает с учёта активный инстанс персонажа на сцене.
        /// </summary>
        public void UnregisterInstance()
        {
            IsPlayerExist = false;
            OnPlayerLost?.Invoke();
        }

        public void SendSuperAttackEvent()
        {
            OnSuperAttacked?.Invoke();
        }

        public void ChangePlayerMode(PlayerMode mode)
        {
            OnPlayerModeChanged?.Invoke(mode);
        }

        public void UpgradeWeaponSize()
        {
            if (_wallet.TrySpend(WeaponSizeUpgradeCost))
            {
                WeaponSizeUpgradeCost += 10;
                WeaponSizeUpgradeLevel++;
                WeaponAdditionalSize = (0.05f * WeaponSizeUpgradeLevel); // TODO добавить поле с множителем

                OnWeaponSizeChanged?.Invoke(WeaponAdditionalSize);
            }
        }
        
        public void UpgradeWeaponPower()
        {
            if (_wallet.TrySpend(WeaponPowerUpgradeCost))
            {
                WeaponPowerUpgradeCost += 10;
                WeaponPowerUpgradeLevel++;
                WeaponAdditionalPower = WeaponPowerUpgradeLevel;  // TODO добавить поле с множителем

                OnWeaponPowerChanged?.Invoke(WeaponAdditionalPower);
            }
        }

        public void ChangeSkin(string newId)
        {
            if (AvailableSkins.Contains(newId))
            {
                SelectedSkinId = newId;
                OnSkinChanged?.Invoke(SelectedSkinId);
            }
            throw new Exception("Попытка установить недоступный скин");
        }

        public void TrySkin(string id)
        {
            OnSkinChanged?.Invoke(id);
        }
        
        public void ChangeWeapon(string newId)
        {
            if (AvailableWeapons.Contains(newId))
            {
                SelectedWeaponId = newId;
                OnSkinChanged?.Invoke(SelectedWeaponId);
            }

            throw new Exception("Попытка установить недоступное оружие");
        }

        public void TryWeapon(string id)
        {
            OnWeaponChanged?.Invoke(id);
        }
        
        public void ChangeCelebration(string newId)
        {
            if (AvailableCelebrations.Contains(newId))
            {
                SelectedCelebrationId = newId;
                OnSkinChanged?.Invoke(SelectedCelebrationId);
            }

            throw new Exception("Попытка установить недоступное празднование");
        }

        public void TryCelebration(string id)
        {
            OnSkinChanged?.Invoke(id);
        }
        
        private void IncreaseWeaponCharge(int additionValue)
        {
            OnChargeIncreased?.Invoke(additionValue);
        }
    }
}