using UnityEngine;

namespace Zparta.CameraLogic
{
    public class BonusCamera : MonoBehaviour
    {
        [SerializeField] private float height = 10f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed;

        [SerializeField] private Transform target;
        
        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.y = height;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); 
            transform.position = smoothedPosition;
        }
    }
}