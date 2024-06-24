using UnityEngine;

namespace Zparta.CameraLogic
{
    public interface ICameraController
    {
        void ConnectToPlayer(Transform player);
        public void Disconect();
        
        void EnableDefaultMode();
        void EnableFlyMode();
        void EnableCelebratingMode();
    }
}