using Enums;
using UnityEngine;

namespace Core.Interface.AchievementSystem
{
    internal interface IAchievement
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Image { get; }
        public AchievementType AchievementType { get; }
        public bool Unlocked { get; }
        public int CurrentValue { get; }
    }
}