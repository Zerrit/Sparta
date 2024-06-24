using System;
using UnityEngine;

namespace Zparta.PlayerLogic.Model
{
    /// <summary>
    /// Предоставляет возможность отслеживать положение персонажа на сцене.
    /// </summary>
    public interface IPlayerHandler
    {
        /// <summary>
        /// Сигнализирует о том, что герой был создан.
        /// </summary>
        public event Action OnPlayerSpawned;

        /// <summary>
        /// Сигнализирует о том, что герой пропал.
        /// </summary>
        public event Action OnPlayerLost;
        
        
        /// <summary>
        /// Существует ли на сцене контролируемый персонаж.
        /// </summary>
        public bool IsPlayerExist { get; }
        
        /// <summary>
        /// Ссылка на трансформ персонажа.
        /// </summary>
        public Transform PlayerTransform { get; }
    }
}