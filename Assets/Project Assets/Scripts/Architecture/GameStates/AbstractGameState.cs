using UnityEngine;

namespace Zparta.Architecture.GameStates
{
    public abstract class AbstractGameState
    {
        public virtual void Enter()
        {
            Debug.Log($"Запущена стадия: {this}");
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void Exit()
        {
            Debug.Log($"Завершена стадия: {this}");
        }
    }
}