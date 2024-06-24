namespace Zparta.Interfaces
{
    /// <summary>
    /// Интерфейс содержит методы обновления, с которыми работает UpdateService.
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Имитирует Fixed Update.
        /// </summary>
        public void PhysicsUpdate();
        
        /// <summary>
        /// Имитирует Update, но вызывается немного реже чем в Update.
        /// </summary>
        public void OptimizedUpdate();
    }
}