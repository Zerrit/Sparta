using UnityEngine;

namespace Zparta.Customization
{
    public interface ICustomizationHandler
    {
        Vector3 PlayerPosition { get; }
        
        void Enable();
        void Disable();
    }

    public class CustomizationHandler : MonoBehaviour, ICustomizationHandler
    {
        public Vector3 PlayerPosition => _rotationPlatform.position;
        
        [SerializeField] private Camera _customizeCamera;
        [SerializeField] private Transform _rotationPlatform;

        public void Enable()
        {
            _rotationPlatform.gameObject.SetActive(true);
            _customizeCamera.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _rotationPlatform.gameObject.SetActive(false);
            _customizeCamera.gameObject.SetActive(false);
        }
    }
}