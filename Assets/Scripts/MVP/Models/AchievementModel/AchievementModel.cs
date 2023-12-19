using System.Collections.Generic;
using Configs;
using static Configs.AchievementListConfig;
using Core.AchievementSystem.AchievementUnlockers;
using Core.Interface.AchievementSystem;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Enums;
using Infrastructure.Extentions;
using IUnlocker = Core.Interface.AchievementSystem.IUnlocker;

namespace MVP.Models.AchievementModel
{
    internal sealed class AchievementModel : IAchievementModel
    {
        private IDataProvider _dataProvider;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private IExpirienceLevelModel _expirienceLevelModel;

        private AchievementListConfig _config;
        private LocationDescriptor[] _locations;

        private Dictionary<IAchievement, bool> _achievementRegister;
        private SerializableDictionary<CharacterName, CharacterConfig> _characterConfigs;
        private List<IUnlocker> _unlockers = new List<IUnlocker>();

        public Dictionary<IAchievement, bool> AchievementRegister
        {
            get => _achievementRegister;
        }
		
        public AchievementModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _achievementRegister = new Dictionary<IAchievement, bool>();

            _config = ResourceLoadManager.GetConfig<AchievementListConfig>();
            _locations = ResourceLoadManager.GetConfig<LocationsPack>().LocationDescriptors;
            _characterConfigs = ResourceLoadManager.GetConfig<PlayerScriptableConfig>().Characters;

            _runStatisticsSystemModel = _dataProvider.RunStatisticsModel;
            _expirienceLevelModel = _dataProvider.LevelModel;
        }


        public void RegisterAchievements()
        {
            for (var i = 0; i < _config.Achievements.Count; i++)
            {
                Achievement achievement = _config.Achievements[i];
                AchievementRegister.Add(achievement, achievement.Unlocked);

                if (!achievement.Unlocked)
                {
                    CreateUnlocker(achievement);
                }
            }
        }

        private void CreateUnlocker(Achievement achievement)
        {
            if (achievement.AchievementType == AchievementType.LevelUp)
            {
                IUnlocker unlocker = new PlayerLevelUpUnlocker(achievement, _runStatisticsSystemModel);
                _unlockers.Add(unlocker);
                _expirienceLevelModel.OnLevelNumberChanged += unlocker.ChangeIntValue;
            }
            else if (achievement.AchievementType == AchievementType.Survivement)
            {
                IUnlocker unlocker = new SurvivementUnlocker(achievement, _runStatisticsSystemModel);
                _unlockers.Add(unlocker);
            }
            else
            {
                IUnlocker unlocker = new EnemyKilledUnlocker(achievement, _runStatisticsSystemModel);
                _unlockers.Add(unlocker);
            }
        }

        public void CheckAchievementState()
        {
            for (var i = 0; i < _unlockers.Count; i++)
            {
                if (_unlockers[i].AchievementData.Unlocked == false)
                {
                    bool unlocked = _unlockers[i].CheckForUnlock();
                    _achievementRegister[_unlockers[i].AchievementData] = unlocked;
                    
                    if (unlocked)
                    {
                        RecieveReward(_unlockers[i].AchievementData);
                    }
                }
            }
        }

        private void RecieveReward(Achievement achievement)
        {
            if (achievement.RewardType == RewardType.LocationUnlock)
            {
                UnlockLocation(achievement);
            }
            else if (achievement.RewardType == RewardType.CharachterUnlock)
            {
                UnlockCharachter(achievement);
            }
        }

        private void UnlockLocation(Achievement achievement)
        {
            foreach (LocationDescriptor location in _locations)
            {
                if (location.LocationName == achievement.RewardValue)
                {
                    location.IsUnlocked = true;
                }
            }
        }

        private void UnlockCharachter(Achievement achievement)
        {
            for(int i = 0; i < _characterConfigs.Length; i++)
            {
                CharacterConfig characterConfig = _characterConfigs.GetValueByIndex(i);
                if (characterConfig.CharacterName == achievement.RewardValue)
                {
                    characterConfig.IsCharachterOpen = true;
                }
            }
        }

        public void Cleanup()
        {
            for(int i = 0; i < _unlockers.Count; i++)
            {
                if (_unlockers[i] is PlayerLevelUpUnlocker)
                {
                    _expirienceLevelModel.OnLevelNumberChanged -= _unlockers[i].ChangeIntValue;
                }
            }
        }
    }
}