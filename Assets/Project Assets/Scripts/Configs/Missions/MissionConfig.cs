using UnityEngine;
using Zparta.MissionLogic;

namespace Zparta.Configs.Missions
{
    [CreateAssetMenu(fileName = "New Mission", menuName = "Configs/New Mission Config")]
    public class MissionConfig : ScriptableObject
    {
        public MissionData Data;
    }
}