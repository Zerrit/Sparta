using UnityEngine;
using Zparta.Levels.Stages;

namespace Zparta.Configs.Levels
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "Configs/Levels/Level Data")]
    public class LevelConfig : ScriptableObject
    {
        [field:SerializeField] 
        public AbstractStage[] StagesQueue { get; private set; }

        [field: SerializeField,
                Header("Требования для получения звёзд за уровень. Сначала указывать для 1 звезды и т.д.")]
        public int[] ScoreTreshholds { get; private set; } = new int[3] {0, 0, 0};
    }
}