using UnityEngine;

namespace Zparta.EnemiesLogic
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Enemy _enemy;
        
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int Fall = Animator.StringToHash("Falling");
        private static readonly int FallingFromEdge = Animator.StringToHash("FallingFromEdge");

        private void Awake()
        {
            _enemy.OnMoving += UpdateMoveAnimationSpeed;
            _enemy.OnFallingFromEdge += ActivateFallingFromEdgeAnimation;
            _enemy.OnFalling += ActivateFallingAnimation;
        }

        private void UpdateMoveAnimationSpeed(float moveSpeed)
            => _animator.SetFloat(MoveSpeed, moveSpeed);
        
        private void ActivateFallingAnimation()
            => _animator.SetTrigger(Fall);

        private void ActivateFallingFromEdgeAnimation()
            => _animator.SetTrigger(FallingFromEdge);
    }
}