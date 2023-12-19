using Core.Interface.IModels;
using static Configs.AchievementListConfig;

namespace Core.AchievementSystem.AchievementUnlockers
{
    internal sealed class PlayerLevelUpUnlocker : BaseUnlocker
    {
        private int _level = 1;

        public PlayerLevelUpUnlocker(Achievement achievement, IRunStatisticsSystemModel stats) : base(achievement, stats) { }

        public override void ChangeIntValue(int level) => _level = level;

        protected override void AssignValueToCompare() => _valueToCompare = _level;
    }
}