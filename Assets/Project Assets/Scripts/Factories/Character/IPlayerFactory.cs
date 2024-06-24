using UnityEngine;
using Zparta.PlayerLogic.View;
using Zparta.Weapons;

namespace Zparta.Factories.Character
{
    public interface IPlayerFactory
    {
        Player Create(Vector3 position, Quaternion rotation, Transform parent = null);
        GameObject CreateSkin(string id, Transform skinHandler);
        AbstractWeapon CreateWeapon(string weaponId, Transform handler);
    }
}