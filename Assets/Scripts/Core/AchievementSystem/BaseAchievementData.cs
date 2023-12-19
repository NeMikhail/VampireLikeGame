using Core.Interface;
using Core.Interface.AchievementSystem;
using Enums;
using UnityEngine;

namespace Core.AchievementSystem
{
    public class BaseAchievementData : IAchievement
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public AchievementType AchievementType { get; private set; }
        [field: SerializeField] public bool Unlocked { get; set; }
        public int CurrentValue { get; set; }

        public RewardType RewardType;
        public string RewardValue;
    }
}