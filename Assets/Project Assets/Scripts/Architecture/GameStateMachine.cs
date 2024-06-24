using System;
using System.Collections.Generic;
using Zenject;
using Zparta.Architecture.GameStates;
using Zparta.Factories;

namespace Zparta.Architecture
{
    /// <summary>
    /// Машина состояний игры.
    /// </summary>
    public class GameStateMachine : IInitializable
    {
        private Dictionary<Type, AbstractGameState> _states;
        private AbstractGameState _currentState;
        
        private readonly GameStateFactory _stateFactory;
        
        public GameStateMachine(GameStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        /// <summary>
        /// Заполняет словарь всеми рабочими состояниями в которое машина сможет переключаться.
        /// </summary>
        public void Initialize()
        {
            _states = new Dictionary<Type, AbstractGameState>()
            {
                [typeof(MenuState)] = _stateFactory.CreateState<MenuState>(),
                [typeof(GameplayState)] = _stateFactory.CreateState<GameplayState>(),
                [typeof(LoseState)] = _stateFactory.CreateState<LoseState>(),
                [typeof(VictoryState)] = _stateFactory.CreateState<VictoryState>(),
                [typeof(CustomizationState)] = _stateFactory.CreateState<CustomizationState>(),
                [typeof(BonusGameState)] = _stateFactory.CreateState<BonusGameState>()
            };
            
            StartMachine<MenuState>();
        }

        /// <summary>
        /// Меняет стейт машины на новый.
        /// </summary>
        /// <typeparam name="T"> Новый стейт. </typeparam>
        public void ChangeState<T>() where T : AbstractGameState
        {
            if (_states.ContainsKey(typeof(T)))
            {
                _currentState?.Exit();
                _currentState = _states[typeof(T)];
                _states[typeof(T)].Enter();
            }
            else throw new Exception($"Не найдено указанное состояние игры: {typeof(T)}");
        }

        /// <summary>
        /// Запускает машину состояний игры в указанное состояние.
        /// </summary>
        /// <typeparam name="T"> Стартовое состояние. </typeparam>
        private void StartMachine<T>() where T : AbstractGameState
        {
            if (_states.ContainsKey(typeof(T)))
            {
                _states[typeof(T)].Enter();
                _currentState = _states[typeof(T)];
            }
            else throw new Exception($"Не найдено указанное состояние игры: {typeof(T)}");
        }
    }
}