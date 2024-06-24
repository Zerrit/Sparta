using System;
using UnityEngine;
using Zparta.PlayerLogic.Model;
using Zparta.Services;

namespace Zparta.PlayerLogic.Input
{
    public class MouseMoveInput : IPlayerInput, IInputBlocker
    {
        public event Action<Vector3> OnMoveDirectionChanged;
        public event Action<bool> OnMoveStatusChanged;
        public event Action OnAttackButtonClicked;
        
        private Vector3 _currentMousePos;
        private bool _isControllable;
        
        private readonly ZpartaInput _input;
        private readonly Camera _camera;
        private readonly IPlayerHandler _playerHandler;
        private readonly IUpdateService _updateService;
        
        public MouseMoveInput(IPlayerHandler playerHandler, IUpdateService updateService)
        {
            _updateService = updateService;
            _playerHandler = playerHandler;
            _input = new ZpartaInput();
            _camera = Camera.main;
            _input.Enable();
            
            _input.Player.SimpleMove.performed += _ => BeginMovement();
            _input.Player.SimpleMove.canceled += _ => StopMovement();
            _input.Player.Attack.performed += _ => Attack();

            _updateService.OnTick += UpdateMoveDirection;
        }
        
        public void BlockInput()
        {
            _input.Disable();
            StopMovement();
            _isControllable = false;
        }

        public void UnblockInput()
        {
            _input.Enable();
            _isControllable = true;
        }

        /// <summary>
        /// Вычисляет направление движения героя относительно курсора.
        /// </summary>
        private void UpdateMoveDirection()
        {
            if(!_isControllable) return;

            TryUpdateMovePoint(_input.Player.MoveDirection.ReadValue<Vector2>());
            
            Vector3 newDirection = (_currentMousePos - _playerHandler.PlayerTransform.position);
            
            if (newDirection.sqrMagnitude > 1) 
                newDirection.Normalize();
            else if(newDirection.sqrMagnitude < .4f)
                newDirection = Vector3.zero;
            
            OnMoveDirectionChanged?.Invoke(newDirection);
        }

        /// <summary>
        /// Пытается получить точку пересечения луча от курсора к навигационному колайдеру.
        /// </summary>
        /// <param name="clickCoords"></param>
        private void TryUpdateMovePoint(Vector2 clickCoords)
        {
            if(!_isControllable) return;
            
            Ray ray = _camera.ScreenPointToRay(clickCoords);

            if (Physics.Raycast(ray, out var hit, 50, 1<<14))
            {
                _currentMousePos = hit.point;
            }
        }
        
        private void BeginMovement()
        {
            if(!_isControllable) return;
            
            OnMoveStatusChanged?.Invoke(true);
        }

        private void StopMovement()
        {
            OnMoveStatusChanged?.Invoke(false);
        }

        private void Attack()
        {
            if(!_isControllable) return;
            
            OnAttackButtonClicked?.Invoke();
        }
    }
}