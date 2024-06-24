using UnityEngine;

namespace Zparta.Weapons
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [field:SerializeField] public string WeaponId { get; private set; }

        public bool IsCharged { get; private set; }
        public bool IsAttackRecovered => Time.time >= +_lastAttackTime + attackCooldown;

        [SerializeField] protected Transform _scalyParent;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected MeshRenderer _meshRenderer;
        [SerializeField] protected ParticleSystem _chargeFX;

        [SerializeField] protected float _attackDuration;
        [SerializeField] protected float _superAttackDuration;
        [SerializeField] protected int pushForce = 15;
        [SerializeField] protected int pushEndForce = 5;
        [SerializeField] protected int fullChargeValue = 100;
        [SerializeField] protected float attackCooldown = .75f;

        private float _lastAttackTime;
        private int _currentCharge;
        private Material _chargeShader;
        
        private static readonly int ChargeValue = Shader.PropertyToID("_ChargeValue");
        private static readonly int SupperAttack = Animator.StringToHash("SuperAttack");
        private static readonly int Attack1 = Animator.StringToHash("Attack");

        private void Awake()
        {
            _animator.writeDefaultValuesOnDisable = true;
            _chargeShader = _meshRenderer.materials[0];
            DischargeWeapon();
        }
        
        public virtual void Attack()
        {
            if (!IsAttackRecovered) return;

            if (IsCharged)
            {
                IsCharged = false;
                _animator.SetTrigger(SupperAttack);
                attackCooldown = _superAttackDuration;
            }
            else
            {
                _animator.SetTrigger(Attack1);
                attackCooldown = _attackDuration;
            }

            _lastAttackTime = Time.time;
        }
        
        public virtual void SetSize(float additionalSize)
        {
            var newSize = 1 + additionalSize;
            _scalyParent.localScale = new Vector3(newSize, 1f, 1f);
        }

        public virtual void BoostSize(float boostValue)
        {
            var newSize = _scalyParent.localScale.x + boostValue;
            _scalyParent.localScale = new Vector3(newSize, 1f, 1f);
        }
        
        public virtual void SetPower(int additionalPower)
        {
            //var newSize = 1 + additionalPower;
            //_scalyParent.localScale = new Vector3(newSize, 1f, 1f);
        }

        public virtual void BoostPower(float boostValue)
        {
            //var newSize = _scalyParent.localScale.x + boostValue;
            //_scalyParent.localScale = new Vector3(newSize, 1f, 1f);
        }
        
        public virtual void Charge(int value)
        {
            if(IsCharged) return;
            
            _currentCharge += value;
            _chargeShader.SetFloat(ChargeValue, (float)_currentCharge/fullChargeValue);
            
            if (_currentCharge >= fullChargeValue)
            {
                ChargeFull();
            }
        }
        
        public virtual void ChargeFull()
        {
            IsCharged = true;
            _chargeShader.SetFloat(ChargeValue, 1f);
            _chargeFX.Play();
        }
        
        public virtual void ResetCharge()
        {
            _currentCharge = 0;
            _chargeShader.SetFloat(ChargeValue, 0f);
        }
        
        public virtual void DischargeWeapon()
        {
            IsCharged = false;
            ResetCharge();
            _chargeFX.Stop();
        }
    }
}