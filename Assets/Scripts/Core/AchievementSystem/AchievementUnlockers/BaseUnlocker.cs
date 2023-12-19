using Configs;
using Core.Interface.IModels;
using Core.ResourceLoader;
using static Configs.AchievementListConfig;
using Enums;
using Core.Interface;

namespace Core.AchievementSystem.AchievementUnlockers
{
    internal class BaseUnlocker : Interface.AchievementSystem.IUnlocker
    {
        private Achievement _achievementData;
        protected readonly IRunStatisticsSystemModel _statistics;
        protected float _valueToCompare;
        protected string _currentPlayerName;

        public Achievement AchievementData { get => _achievementData; }

        public BaseUnlocker(Achievement achievement, IRunStatisticsSystemModel statistics)
        {
            _achievementData = achievement;
            _statistics = statistics;
            _currentPlayerName = ResourceLoadManager.GetConfig<PlayerScriptableConfig>().CurrentCharacterName.ToString();
        }


        public virtual void ChangeIntValue(int level) { }

        protected virtual void AssignValueToCompare() { }

        public bool CheckForUnlock()
        {
            AssignValueToCompare();
            switch (AchievementData.ConditionType)
            {
                case ConditionType.Player:
                    if (_valueToCompare >= AchievementData.Value && AchievementData.ConditionName == _currentPlayerName)
                        AchievementData.Unlocked = true;
                    return AchievementData.Unlocked;
                case ConditionType.Location:
                    if (_valueToCompare >= AchievementData.Value && AchievementData.ConditionName == _statistics.LocationName)
                        AchievementData.Unlocked = true;
                    return AchievementData.Unlocked;
                case ConditionType.None:
                    if (_valueToCompare >= AchievementData.Value)
                        AchievementData.Unlocked = true;
                    return AchievementData.Unlocked;
                default:
                    UnityEngine.Debug.Log("This condition is not registered yet");
                    return false;
            }
        }
    }
}