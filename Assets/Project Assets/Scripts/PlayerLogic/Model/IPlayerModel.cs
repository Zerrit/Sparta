using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zparta.PlayerLogic.Model
{
    public interface IPlayerModel
    {
        event Action<Vector3> OnPlayerPlacementRequested;
        event Action OnPlayerRemovalRequested;

        event Action OnSuperAttacked;
        event Action<PlayerMode> OnPlayerModeChanged;
        event Action<int> OnChargeIncreased;
        event Action<float> OnWeaponSizeChanged;
        event Action<int> OnWeaponPowerChanged;

        event Action<string> OnSkinChanged;
        event Action<string> OnWeaponChanged;
        event Action<string> OnSelebrationChanged;

        //--Customize Properties
        string SelectedSkinId { get; }
        List<string> AvailableSkins { get; }
        
        string SelectedWeaponId { get; }
        List<string> AvailableWeapons { get; }
        
        string SelectedCelebrationId { get; }
        List<string> AvailableCelebrations { get; }
        
        //--Weapon Properties
        int WeaponAdditionalPower { get; }
        float WeaponAdditionalSize { get; }

        int WeaponSizeUpgradeLevel { get; }
        int WeaponSizeUpgradeCost { get; }
        bool IsWeaponSizeUpgradeAvailable { get; }

        int WeaponPowerUpgradeLevel { get; }
        int WeaponPowerUpgradeCost { get; }
        bool IsWeaponPowerUpgradeAvailable { get; }

        //--Methods--
        
        void PlacePlayer(Vector3 spawnPos);
        void RemovePlayer();

        void RegisterInstance(Transform playerTransform);
        void UnregisterInstance();

        void SendSuperAttackEvent();
        void ChangePlayerMode(PlayerMode mode);

        void UpgradeWeaponSize();
        void UpgradeWeaponPower();

        void TrySkin(string id);
        void ChangeSkin(string newId);
        void TryWeapon(string id);
        void ChangeWeapon(string newId);
        void TryCelebration(string id);
        void ChangeCelebration(string newId);
    }
}