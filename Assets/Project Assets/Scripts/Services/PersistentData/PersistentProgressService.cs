using Zparta.Data;

namespace Zparta.Services.PersistentData
{
    public class PersistentProgressService : IProgressProvider
    {
        public PlayerProgress Progress { get; set; }

        public PersistentProgressService()
        {
            //TRY LOAD DATA OR CREATE NEW;
            Progress = new PlayerProgress(0, 0);
        }
    }
}