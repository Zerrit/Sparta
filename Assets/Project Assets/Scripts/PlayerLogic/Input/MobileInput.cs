using System;
using UnityEngine;
using Zparta.Factories.Joysticks;
using Zparta.Joystick_Pack.Scripts.Base;
using Zparta.PlayerLogic.Input;

namespace Zparta.PlayerLogic.InputControllers
{
    public class MobileInput : IPlayerInput, IInputBlocker
    {
        public event Action<Vector3> OnMoveDirectionChanged;
        public event Action<bool> OnMoveStatusChanged;
        public event Action OnAttackButtonClicked;

        private bool _isMoving;
        private bool _isControllable;
        
        private readonly Joystick _mobileInput;
        
        
        public MobileInput(IJoystickFactory joystickFactory)
        {
            _mobileInput = joystickFactory.CreteJoystick();
            
            _mobileInput.OnInputChanged += ChangeMoveDirection;
            _mobileInput.OnInputCanceled += StopMovement;
            _mobileInput.OnAttackClick += Attack;
        }
        
        void IInputBlocker.BlockInput()
        {
            _mobileInput.enabled = false;
            _isControllable = false;
            StopMovement();
        }

        void IInputBlocker.UnblockInput()
        {
            _mobileInput.enabled = true;
            _isControllable = true;
        }

        
        private void ChangeMoveDirection(Vector2 direction)
        {
            if(!_isControllable) 
                return;

            if (!_isMoving)
            {
                _isMoving = true;
                OnMoveStatusChanged?.Invoke(true);
            }

            Vector3 newDirection = new Vector3(direction.x, 0f, direction.y);
            OnMoveDirectionChanged?.Invoke(newDirection);
        }

        private void StopMovement()
        {
            _isMoving = false;
            OnMoveStatusChanged?.Invoke(false);
        }
        
        private void Attack()
        {
            if(!_isControllable) return;
            
            OnAttackButtonClicked?.Invoke();
        }
    }
}