using System;
using UnityEngine;

namespace Zparta.PlayerLogic.Input
{
    /// <summary>
    /// Интерфейс для подключения персонажа к инпутам игрока.
    /// </summary>
    public interface IPlayerInput
    {
        public event Action<Vector3> OnMoveDirectionChanged;
        public event Action<bool> OnMoveStatusChanged;
        public event Action OnAttackButtonClicked;
    }
}