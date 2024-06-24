using UnityEngine;

namespace Zparta.Services
{
    public interface ICameraService
    {
    }

    public class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera gameplayCamera;
        [SerializeField] private Camera bonusCamera;
    
        public CameraService()
        {
        }
    }
}