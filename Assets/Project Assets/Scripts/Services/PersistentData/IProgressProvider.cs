using Zparta.Data;

namespace Zparta.Services.PersistentData
{
    public interface IProgressProvider
    {
        public PlayerProgress Progress { get; set; }
    }
}