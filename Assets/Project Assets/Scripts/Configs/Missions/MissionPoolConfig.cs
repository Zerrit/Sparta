using System;
using UnityEngine;
using Zparta.MissionLogic;

namespace Zparta.Configs.Missions
{
    [CreateAssetMenu(fileName = "MissionPoolConfig", menuName = "Configs/MissionPool")]
    public class MissionPoolConfig : ScriptableObject
    {
        [field:SerializeField] public MissionConfig[] Quests { get; private set; }

        public MissionData GetData(string id)
        {
            foreach (var quest in Quests)
            {
                if (quest.Data.MissionId == id)
                {
                    return quest.Data;
                }
            }

            throw new Exception($"Не найдено конфига для квеста с id {id}");
        }
    }
}