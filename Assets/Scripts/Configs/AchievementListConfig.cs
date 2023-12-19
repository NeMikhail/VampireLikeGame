using System.Collections.Generic;
using UnityEngine;
using Core.AchievementSystem;
using Enums;

namespace Configs
{
    [CreateAssetMenu(fileName = "AchievementList", menuName = "Configs /Achievements /AchievementList")]
    public sealed class AchievementListConfig : ScriptableObject
    {
        [System.Serializable]
        public sealed class Achievement : BaseAchievementData
        {
            [Space]
            public ConditionType ConditionType;
            public string ConditionName;
            public int Value;
            [Space]
            public EnemyType EnemyType;
            public WeaponsName WeaponsName;
        }

        public List<Achievement> Achievements;

        public void LockAchievements()
        {
            foreach (Achievement achievement in Achievements)
            {
                achievement.Unlocked = false;
            }
        }
    }
}