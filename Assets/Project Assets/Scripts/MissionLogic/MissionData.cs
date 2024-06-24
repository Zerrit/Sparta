using System;
using UnityEngine;

namespace Zparta.MissionLogic
{
    [Serializable]
    public struct MissionData
    {
        [field: SerializeField] public string MissionId { get; private set; }
        [field: SerializeField] public string DescriptionLocId { get; private set; }
        [field: SerializeField] public int RewardValue { get; private set; }
        [field: SerializeField] public int RequirmentValue { get; private set; }

        public int CurrentProgress { get; set; }
    }
}