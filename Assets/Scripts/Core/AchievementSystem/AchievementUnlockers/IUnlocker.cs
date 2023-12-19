using static Configs.AchievementListConfig;


namespace Core.AchievementSystem.AchievementUnlockers
{
    internal interface IUnlocker
    {
        Achievement AchievementData { get; }
        void ChangeIntValue(int level);
        bool CheckForUnlock();
    }
}