using System.Collections.Generic;
using Configs;
using static Configs.AchievementListConfig;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Enums;
using Infrastructure.Extentions;

namespace MVP.Presenters.AchievementPresenter
{
    internal sealed class AchievementUnlockPresenter : IInitialisation
    {
        private SerializableDictionary<CharacterName, CharacterConfig> _characterConfigs;
        private LocationDescriptor[] _locations;
        private List<Achievement> _achievementList;

        internal AchievementUnlockPresenter()
        {
            AchievementListConfig achievementConfig = ResourceLoadManager.GetConfig<AchievementListConfig>();
            _locations = ResourceLoadManager.GetConfig<LocationsPack>().LocationDescriptors;
            _characterConfigs = ResourceLoadManager.GetConfig<PlayerScriptableConfig>().Characters;
            _achievementList = achievementConfig.Achievements;
        }
        
        public void Initialisation()
        {
            CheckAchievements();
        }
        
        private void CheckAchievements()
        {
            foreach (Achievement achievement in _achievementList)
            {
                if (achievement.Unlocked)
                {
                    RecieveReward(achievement);
                }
                else
                {
                    LockReward(achievement);
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

        private void LockReward(Achievement achievement)
        {
            if (achievement.RewardType == RewardType.LocationUnlock)
            {
                LockLocation(achievement);
            }
            else if (achievement.RewardType == RewardType.CharachterUnlock)
            {
                LockCharachter(achievement);
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
        
        private void LockLocation(Achievement achievement)
        {
            foreach (LocationDescriptor location in _locations)
            {
                if (location.LocationName == achievement.RewardValue)
                {
                    location.IsUnlocked = false;
                }
            }
        }

        private void LockCharachter(Achievement achievement)
        {
            for(int i = 0; i < _characterConfigs.Length; i++)
            {
                CharacterConfig characterConfig = _characterConfigs.GetValueByIndex(i);
                if (characterConfig.CharacterName == achievement.RewardValue)
                {
                    characterConfig.IsCharachterOpen = false;
                }
            }
        }
    }
}