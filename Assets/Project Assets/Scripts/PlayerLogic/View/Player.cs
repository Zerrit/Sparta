using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Zparta.Interfaces;
using Zparta.PlayerLogic.Input;
using Zparta.PlayerLogic.Model;
using Zparta.Services;
using Zparta.Weapons;

namespace Zparta.PlayerLogic.View
{
    public class Player : MonoBehaviour, IUpdatable, IPushable
    {
        public event Action OnSuperAttackEnabled;
        public event Action<float> OnMoving;
        public event Action OnFalling;
        public event Action OnCelebrating;
        public event Action OnDespawned;
        
        [field:SerializeField] public Transform SelfTransform { get; private set; }
        
        public AbstractWeapon Weapon;
        public Transform SkinHandler;
        public Transform WeaponHandler;
        
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _speed;
        [SerializeField] private float _gravityScale;
        [SerializeField] private float _turnSmoothTime;
        
        private bool _isDisabled;
        private bool _isMoveng;
        private bool _isFalling;
        
        private Vector3 _moveDir;
        public float _targetAngle;
        private int _deadlyDepth = -5;
        
        private IPlayerInput _playerInput;
        private IUpdateService _updateService;

        [Inject]
        public void Construct(IPlayerInput playerInput, IUpdateService updateService)
        {
            _playerInput = playerInput;
            _updateService = updateService;
        }

        private void OnEnable()
        {
            _isFalling = false;
            _isDisabled = false;
            _isMoveng = false;

            _updateService.AddToList(this);

            _playerInput.OnMoveDirectionChanged += ChangeMoveDirection;
            _playerInput.OnMoveStatusChanged += SwitchMovement;
            _playerInput.OnAttackButtonClicked += UseWeapon;
        }

        private void OnDisable()
        {
            _updateService.RemoveFromList(this);

            _playerInput.OnMoveDirectionChanged -= ChangeMoveDirection;
            _playerInput.OnMoveStatusChanged -= SwitchMovement;
            _playerInput.OnAttackButtonClicked -= UseWeapon;
        }

        void IUpdatable.PhysicsUpdate()
        {
            MovePlayer();
            RotateHero();
        }

        void IUpdatable.OptimizedUpdate()
        {
            CheckGround();
        }

        void IPushable.Push(Vector3 velocityVector, bool resetVelocity)
        {
            _rigidBody.AddForce(velocityVector, ForceMode.VelocityChange);
        }

        public void Refresh()
        {
            SelfTransform.rotation = Quaternion.identity;
            _rigidBody.rotation = Quaternion.identity;
            _targetAngle = 0;
            _moveDir = Vector3.forward;
            Weapon.DischargeWeapon();
        }

        public void ChangeMode(PlayerMode mode)
        {
            switch (mode)
            {
                case PlayerMode.Default:
                {
                    WeaponHandler.gameObject.SetActive(true);
                    break;
                }
                case PlayerMode.Celebrating:
                {
                    _moveDir = Vector3.back;
                    _rigidBody.rotation = Quaternion.Euler(0f, 180f, 0f);
                    WeaponHandler.gameObject.SetActive(false);
                    OnCelebrating?.Invoke();
                    break;
                }
            }
        }

        public void UpdateWeaponCharge(int value)
        {
            Weapon.Charge(value);
        }

        public void BoostSize(float size)
        {
            // Увеличить размер локально.
        }

        public void BoostCharge(float value)
        {
            Weapon.ChargeFull();// Заряжать на определённый процент
        }

        public void Despawn()
        {
            OnDespawned?.Invoke();
            gameObject.SetActive(false);
        }
        
        //-------------------WEAPON--------------------------//

        /// <summary>
        /// Вызывает метод атаки у экипированного оружия. В случае если оружие заряжено - вызывает соответствующие методы.
        /// </summary>
        private void UseWeapon()
        {
            if(Weapon == null || _isDisabled)
                return;
            
            if (Weapon.IsCharged & Weapon.IsAttackRecovered)
            {
                StartCoroutine(Disable(1.5f));
                OnSuperAttackEnabled?.Invoke();
            }
            Weapon.Attack();
        }
        
        //-------------------MOVEMENT--------------------------//
        
        private void ChangeMoveDirection(Vector3 direction)
        {
            _moveDir = direction;
        }

        private void SwitchMovement(bool isMoving)
        {
            _isMoveng = isMoving;

            if (!_isMoveng)
            {
                OnMoving?.Invoke(0f);
            }
        }
        
        private void MovePlayer()
        {
            float moveSpeed = _rigidBody.velocity.magnitude / 3;
            OnMoving?.Invoke(moveSpeed);
            
            if(_isDisabled || !_isMoveng) return;
            
            _rigidBody.AddForce(_moveDir * _speed, ForceMode.Acceleration);
        }
        
        private void RotateHero()
        {
            //if(_isDisabled || !_isMoveng) return; // INFO Mobile mode
            if(_isDisabled) return;
            
            _targetAngle = Mathf.Atan2(_moveDir.x,_moveDir.z) * Mathf.Rad2Deg;
            _rigidBody.MoveRotation(Quaternion.Slerp(SelfTransform.rotation, Quaternion.Euler(0f, _targetAngle, 0f), _turnSmoothTime * Time.fixedDeltaTime));
        }
        
        private void CheckGround()
        {
            if(_isFalling) return;
            
            if (!Physics.Raycast(SelfTransform.position + Vector3.up, Vector3.down, 3f, _groundLayer.value))
            {
                StartCoroutine(TryConfirmFalling());
            }
        }

        //-------------------COROUTINES--------------------------//

        /// <summary>
        /// Инициирует дополнительную проверку падения и при подтвердлении осуществляет приземление игрока вниз.
        /// </summary>
        /// <returns></returns>
        private IEnumerator TryConfirmFalling()
        {
            _isFalling = true;
            int checkCounter = 0;

            while (checkCounter < 3)
            {
                if (!Physics.Raycast(SelfTransform.position + Vector3.up, Vector3.down, 3f, _groundLayer.value))
                {
                    checkCounter++;
                    yield return null;
                }
                else
                {
                    _isFalling = false;
                    yield break;
                }
            }

            OnFalling?.Invoke();
            _isDisabled = true;
            _rigidBody.velocity = Vector3.zero;
            
            while (SelfTransform.position.y > _deadlyDepth)
            {
                _rigidBody.AddForce(Vector3.down * _gravityScale, ForceMode.Acceleration);
                yield return null;
            }
            
            Despawn();
            _isFalling = false;
        }
        
        /// <summary>
        /// Блокирует движение героя на заданный промежуток времени.
        /// </summary>
        /// <param name="disableTime"> Время блокировки. </param>
        private IEnumerator Disable(float disableTime)
        {
            _isDisabled = true;
            
            yield return new WaitForSeconds(disableTime);
            _isDisabled = false;
        }
    }
}
