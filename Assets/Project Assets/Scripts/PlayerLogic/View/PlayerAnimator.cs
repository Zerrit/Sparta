using System;
using UnityEngine;

namespace Zparta.PlayerLogic.View
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;
        
        
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Celebrating = Animator.StringToHash("Celebrating");

        private void OnEnable()
        {
            _player.OnMoving += UpdateMoveAnimationSpeed;
            _player.OnFalling += ActivateFallingAnimation;
            _player.OnCelebrating += ActivateCelebratingAnimation;
        }
        
        private void OnDisable()
        {
            _player.OnMoving -= UpdateMoveAnimationSpeed;
            _player.OnFalling -= ActivateFallingAnimation;
            _player.OnCelebrating -= ActivateCelebratingAnimation; 
        }

        private void UpdateMoveAnimationSpeed(float moveSpeed)
            => _animator.SetFloat(MoveSpeed, moveSpeed);

        private void ActivateFallingAnimation()
            => _animator.SetTrigger(Fall);

        private void ActivateCelebratingAnimation()
            => _animator.SetTrigger(Celebrating);
    }
}