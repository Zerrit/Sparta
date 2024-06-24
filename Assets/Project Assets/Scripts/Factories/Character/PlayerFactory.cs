using System;
using UnityEngine;
using Zenject;
using Zparta.Configs.Skins;
using Zparta.Configs.Weapons;
using Zparta.PlayerLogic.View;
using Zparta.Weapons;
using Object = UnityEngine.Object;

namespace Zparta.Factories.Character
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly Player _playerPrefab;
        private readonly AllSkinsConfig _skinConfig;
        private readonly AllWeaponsConfig _weaponConfig;

        public PlayerFactory(DiContainer container, Player playerPrefab, AllSkinsConfig skinConfig, AllWeaponsConfig weaponConfig)
        {
            _playerPrefab = playerPrefab;
            _container = container;
            _skinConfig = skinConfig;
            _weaponConfig = weaponConfig;
        }

        public Player Create(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return _container.InstantiatePrefabForComponent<Player>(_playerPrefab, position, rotation, parent);
        }
        
        public GameObject CreateSkin(string id, Transform skinHandler)
        {
            var skinPrefab = Array.Find(_skinConfig.Skins, x => x.Id == id).Prefab;
            
            if (skinPrefab)
            {
                return _container.InstantiatePrefab(skinPrefab, skinHandler);
            }
            throw new Exception($"Не обнаружен префаб со следующим id: {id}");
        }
        
        public AbstractWeapon CreateWeapon(string weaponId, Transform handler)
        {
            var prefab = Array.Find(_weaponConfig.Weapons, x => x.Id == weaponId).Weapons;

            if (prefab)
            {
                return Object.Instantiate(prefab, handler);
            }

            throw new Exception($"Не обнаружен префаб со следующим id: {weaponId}");
        }
    }
}