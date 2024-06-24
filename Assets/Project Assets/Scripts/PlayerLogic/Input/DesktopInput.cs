using System;
using UnityEngine;
using Zparta.PlayerLogic.Input;

namespace Zparta.PlayerLogic.InputControllers
{
    public class DesktopInput: IPlayerInput, IInputBlocker
    {
        public event Action<Vector3> OnMoveDirectionChanged;
        public event Action<bool> OnMoveStatusChanged;
        public event Action OnAttackButtonClicked;
        
        private readonly ZpartaInput _input;
        
        
        public DesktopInput()
        {
            _input = new ZpartaInput();
            _input.Enable();
            
            /*_input.Player.TestMovement.performed += (value) => ChangeMoveDirection(value.ReadValue<Vector2>());
            _input.Player.TestMovement.canceled += _ => StopMovement();
            _input.Player.Attack.performed += _ => Attack();*/
        }
        
        
        public void BlockInput()
        {
            _input.Disable();
            //_isCanControllable = false;
            //StopMovement();
        }

        public void UnblockInput()
        {
            _input.Enable();
            //_isCanControllable = true;
        }
    }
}