using UnityEngine;

namespace Zparta.Interfaces
{
    /// <summary>
    /// Интерфейс для сущностей, которые можно толкнуть.
    /// </summary>
    public interface IPushable
    {
        /// <summary>
        /// Толкает данную сущность.
        /// </summary>
        /// <param name="velocityVector"> Направление в котором произойдёт точнок. </param>
        /// <param name="resetVelocity"></param>
        public void Push(Vector3 velocityVector, bool resetVelocity = false);
    }
}