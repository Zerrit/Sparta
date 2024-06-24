using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using Zenject;
using Zparta.Interfaces;
using Zparta.PlayerLogic.Model;
using Zparta.Services;

namespace Zparta.EnemiesLogic
{
    [RequireComponent(typeof(Rigidbody),typeof(Seeker))]
    public class Enemy : MonoBehaviour, IUpdatable, IPushable, IEnemy
    {
        public event Action<float> OnMoving;
        public event Action OnFalling;
        public event Action OnFallingFromEdge;
        
        public event Action<Enemy> OnKilled;

        public int RewardValue => rewardValue;
        
        [SerializeField] private Transform selfTransform;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Collider collider;
        [SerializeField] private Seeker seeker;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private int _fallingCheckTreshhold = 3;
        [SerializeField] private int pushStrengеh = 15;
        [SerializeField] private int agroDistance = 10;
        [SerializeField] private float speed;
        [SerializeField] private float gravityScale;
        [SerializeField] private float turnSmoothTime;
        [SerializeField] private float disableTime = 2.5f;
        [SerializeField] private int deadlyDepth = -8;
        [SerializeField] private int rewardValue = 1;
        
        private bool _isActivated;
        private bool _isSleep;
        private bool _isDisabled;
        private bool _isFallingFromEdge;

        private Transform _player;
        private Vector3 _moveDir;
        private float _targetAngle;
        private Vector3 _cumulativeForce = Vector3.zero;
        private int _currentWaypoint;
        private Path _path;
        private IUpdateService _updateService;
        private IPlayerHandler _playerHandler;

        
        [Inject]
        public void Construct(IUpdateService updateService, IPlayerHandler playerHandler)
        {
            _updateService = updateService;
            _playerHandler = playerHandler;
        }
        
        public void OnEnable()
        {
            _isActivated = false;
            _isSleep = false;
            _isDisabled = false;
            _isFallingFromEdge = false;
            
            rigidBody.velocity = Vector3.zero;
            rigidBody.rotation = Quaternion.Euler(0f, 180f, 0f);
            _updateService.AddToList(this);
            OnMoving?.Invoke(0f);
            
            StartCoroutine(AppearingDelay());
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Weapon"))
            {
                if(!_isDisabled)
                    StartCoroutine(Disable());
            }
            else if (other.collider.CompareTag("Player"))
            {
                if(other.gameObject.TryGetComponent(out IPushable player))
                {
                    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                    pushDirection.y = 0;
                    player.Push(pushDirection * pushStrengеh);
                }
            }
        }

        
        void IUpdatable.OptimizedUpdate()
        {
            CheckGround();
            UpdatePath();
        }

        void IUpdatable.PhysicsUpdate()
        {
            MoveToPlayer();
            RotateToTarget();
            //ApplyPushForce();
        }

        void IPushable.Push(Vector3 velocityVector, bool resetVelocity)
        {
            if(resetVelocity) rigidBody.velocity = Vector3.zero;
            
            rigidBody.AddForce(velocityVector, ForceMode.VelocityChange);
            
            if(!_isDisabled)
                StartCoroutine(Disable());
        }

        void IEnemy.Activate()
        {
            _isActivated = true;
        }
        
        public void Kill()
        {
            gameObject.SetActive(false);
            OnKilled?.Invoke(this);
        }
        
        
        /// <summary>
        /// Проверяет находится ли объект на поверхности. Если нет, то начинает толкать себя вниз до тех пока не преодолеет указанный барьер, после чего выключается.
        /// </summary>
        private void CheckGround()
        {
            if(_isFallingFromEdge) return;

            if (!Physics.Raycast(selfTransform.position + Vector3.up, Vector3.down, 3f, groundLayer.value))
            {
                StartCoroutine(TryConfirmFalling());
            }
        }
        
        /// <summary>
        /// Перемещает объект по направлению к ближайшей точке навигационного пути.
        /// </summary>
        private void MoveToPlayer()
        {
            float moveSpeed = rigidBody.velocity.magnitude / speed;
            OnMoving?.Invoke(moveSpeed);
            
            if(_isSleep | _isDisabled | _path == null) return;

            if (_currentWaypoint >= _path.vectorPath.Count) return;
            
            _moveDir = (_path.vectorPath[_currentWaypoint] - selfTransform.position).normalized;
            _moveDir.y = 0;
            
            rigidBody.AddForce(_moveDir * speed, ForceMode.Acceleration);
            float distance = Vector3.Distance(selfTransform.position, _path.vectorPath[_currentWaypoint]);

            if (distance < 1f) _currentWaypoint++;
        }

        /// <summary>
        /// Поворачивает объект в направлении движения.
        /// </summary>
        private void RotateToTarget()
        {
            if(_moveDir.sqrMagnitude < 0.001f | _isDisabled | _isSleep) return;
            
            _targetAngle = Mathf.Atan2(_moveDir.x,_moveDir.z) * Mathf.Rad2Deg;
            rigidBody.MoveRotation(Quaternion.Slerp(selfTransform.rotation, Quaternion.Euler(0f, _targetAngle, 0f), turnSmoothTime * Time.fixedDeltaTime));
        }
        
        /// <summary>
        /// Обновляет навигационный путь.
        /// </summary>
        private void UpdatePath()
        {
            if(!seeker.IsDone() | _isDisabled | _isSleep) return;

            if (_playerHandler.IsPlayerExist)
                seeker.StartPath(selfTransform.position, _player.position, OnPathUpdate);
            else
                StartCoroutine(Euthanize());
        }
        
        /// <summary>
        /// Коллбэк для обработки вычисленного навигационного пути.
        /// </summary>
        /// <param name="path"> Новый путь. </param>
        private void OnPathUpdate(Path path)
        {
            if (path.error) return;
            
            _path = path;
            _currentWaypoint = 1;
        }

        
        /// <summary>
        /// Пытается получить ссылку на Transform игрока.
        /// </summary>
        private bool TryGetPlayer()
        {
            if (_playerHandler.IsPlayerExist)
            {
                _player = _playerHandler.PlayerTransform;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверяет находится ли игрок достаточно близко для начала преследования.
        /// </summary>
        private bool IsPlayerNearby()
        {
            if (Vector3.Distance(selfTransform.position, _player.position) <= agroDistance)
                return true;

            return false;
        }

        /// <summary>
        /// Инициирует дополнительную проверку падения и при подтвердлении осуществляет приземление игрока вниз.
        /// </summary>
        /// <returns></returns>
        private IEnumerator TryConfirmFalling()
        {
            _isFallingFromEdge = true;
            int checkCounter = 0;

            while (checkCounter < _fallingCheckTreshhold)
            {
                if (!Physics.Raycast(selfTransform.position + Vector3.up, Vector3.down, 3f, groundLayer.value))
                {
                    checkCounter++;
                    yield return null;
                }
                else
                {
                    _isFallingFromEdge = false;
                    yield break;
                }
            }

            OnFallingFromEdge?.Invoke();
            _isDisabled = true;
            
            while (selfTransform.position.y > deadlyDepth)
            {
                rigidBody.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
                yield return null;
            }

            Kill();
            _isFallingFromEdge = false;
        }
        

        /// <summary>
        /// Блокирует юнита до окончания анимации появления.
        /// </summary>
        private IEnumerator AppearingDelay()
        {
            _isDisabled = true;
            rigidBody.isKinematic = true;
            collider.enabled = false;

            yield return new WaitForSeconds(2f);
            
            collider.enabled = true;
            rigidBody.isKinematic = false;
            _isDisabled = false;
            
            StartCoroutine(Euthanize());
        }
        
        /// <summary>
        /// Сопрограмма выводит из строя юнита на фиксированное время.
        /// </summary>
        /// <returns></returns>S
        private IEnumerator Disable()
        {
            _isDisabled = true;
            OnFalling?.Invoke();
            
            yield return new WaitForSeconds(disableTime);
            _isDisabled = false;
        }
        
        /// <summary>
        /// Сопрограмма вводит юнита в состояние сна, до момента когда игрок не появится и не окажется в зоне агрессии.
        /// </summary>
        private IEnumerator Euthanize()
        {
            _isSleep = true;
            
            yield return new WaitUntil(() => _isActivated);
            
            while (!TryGetPlayer())
            {
                yield return new WaitForSeconds(.5f);
            }
            
            while (!IsPlayerNearby())
            {
                yield return new WaitForSeconds(.4f);
            }
            _isSleep = false;
        }

        private void OnDisable()
        {
            _updateService.RemoveFromList(this);
        }
    }
}