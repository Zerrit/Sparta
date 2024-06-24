using System.Collections;
using UnityEngine;
using Zenject;
using Zparta.PlayerLogic.Model;

namespace Zparta.CameraLogic
{
    public class LevelsCamera : MonoBehaviour
    {
        [SerializeField] private float _defaultHeight = 20f;
        [SerializeField] private int _defaultXSlope = 55;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed;
        
        [SerializeField] private AnimationCurve _approachingCurve;

        [SerializeField] private float _victoryHeight = 5;
        [SerializeField] private int _victoryXSlope = 7;

        private float _currentHeight;
        private bool _isPlayerExist;
        private Transform _playerTransform;
        
        private IPlayerModel _playerModel;
        private IPlayerHandler _playerHandler;
        
        [Inject]
        public void Construct(IPlayerModel playerModel, IPlayerHandler playerHandler)
        {
            _playerModel = playerModel;
            _playerHandler = playerHandler;

            Initialize();
        }

        private void Initialize()
        {
            _currentHeight = _defaultHeight;

            _playerHandler.OnPlayerSpawned += ConnectToPlayer;
            _playerHandler.OnPlayerLost += Disconect;

            _playerModel.OnPlayerModeChanged += SwitchMode;
            _playerModel.OnSuperAttacked += EnableFlyMode;
        }


        private void FixedUpdate()
        {
            if (!_isPlayerExist) return;

            Vector3 desiredPosition = _playerTransform.position + _offset;
            desiredPosition.y = _currentHeight;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }

        public void ConnectToPlayer()
        {
            _playerTransform = _playerHandler.PlayerTransform;
            _isPlayerExist = true;
        }

        public void Disconect()
        {
            _isPlayerExist = false;
        }
        
        private void SwitchMode(PlayerMode mode)
        {
            switch (mode)
            {
                case PlayerMode.Default:
                {
                    _currentHeight = _defaultHeight;                                   
                    transform.rotation = Quaternion.Euler(_defaultXSlope, 0f, 0f);     
                    break;
                }
                case PlayerMode.Celebrating:
                {
                    _currentHeight = _victoryHeight; 
                    StartCoroutine(ChangeXSlope());  
                    break;
                }
            }
        }

        
        [ContextMenu("FlyCamera")]
        public void EnableFlyMode()
        {
            StartCoroutine(Approach());
        }

        private IEnumerator Approach()
        {
            float elapsedTime = 0f;

            while (elapsedTime <= 1f)
            {
                _currentHeight = _defaultHeight * _approachingCurve.Evaluate(elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _currentHeight = _defaultHeight;
        }

        private IEnumerator ChangeXSlope()
        {
            var targetRotation = Quaternion.Euler(_victoryXSlope, 0f, 0f);
            
            while (transform.eulerAngles.x - _victoryXSlope > 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 40);
                yield return null;
            }
        }
    }
}