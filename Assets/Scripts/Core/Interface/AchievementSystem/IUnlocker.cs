using static Configs.AchievementListConfig;

namespace Core.Interface.AchievementSystem
{
    internal interface IUnlocker
    {
        public Achievement AchievementData { get; }
        public void ChangeIntValue(int level);
        public bool CheckForUnlock();
    }
}