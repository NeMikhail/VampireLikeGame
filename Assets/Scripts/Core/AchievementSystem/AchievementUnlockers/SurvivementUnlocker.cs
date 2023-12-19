using Core.Interface.IModels;
using static Configs.AchievementListConfig;

namespace Core.AchievementSystem.AchievementUnlockers
{
    internal sealed class SurvivementUnlocker : BaseUnlocker
    {
        public SurvivementUnlocker(Achievement surAch, IRunStatisticsSystemModel stats) : base(surAch, stats) { }

        protected override void AssignValueToCompare() => _valueToCompare = _statistics.TotalTime;
    }
}