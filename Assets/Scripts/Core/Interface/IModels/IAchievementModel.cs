using System.Collections.Generic;
using Core.Interface.AchievementSystem;

namespace Core.Interface.IModels
{
    internal interface IAchievementModel
    {
        public Dictionary<IAchievement, bool> AchievementRegister { get; }
        public void RegisterAchievements();
        public void CheckAchievementState();
        public void Cleanup();
    }
}