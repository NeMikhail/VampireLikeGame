using Audio.Service;
using UnityEngine;
using Configs;
using static Configs.AchievementListConfig;
using Core.Interface.ISavers;
using System.Collections.Generic;

namespace Core.SaveClass.SaveModules
{
    internal sealed class PlayerPrefsParserModule : ISave
    {
        private const string ACHIEVEMENT_PREFIX = "a_";
        private const string CHARACTER_PREFIX = "c_";
        private const string STATISTICS_PREFIX = "stat_";
        private const string UPGRADE_PREFIX = "u_";
        private const string COINS_KEY = "coins";
        private const string UPGRADE_COINS_KEY = "upgrade_coins";
        private const string MASTER_VOLUME_KEY = "s_masterVolume";
        private const string MUSIC_VOLUME_KEY = "s_musicVolume";
        private const string EFFECTS_VOLUME_KEY = "s_effectsVolume";
        private const string TOTAL_TIME_KEY = "stat_time";
        private const string TOTAL_COINS_KEY = "stat_coins";
        private const string TOTAL_KILLS_KEY = "stat_kills";
        private const string RUN_COUNT_KEY = "stat_run_count";
        private const string UPRGADE_MODIFIER_KEY = "upgrade_modifiers";

        public void TryLoad(ConfigLoader configLoader)
        {
            MetaprogressionConfig metaprogression = GetScriptable<MetaprogressionConfig>(configLoader);
            AchievementListConfig achievementList = metaprogression.AchievementList;
            GlobalStatisticConfig globalStatisticConfig = GetScriptable<GlobalStatisticConfig>(configLoader);
            PlayerScriptableConfig charachters = GetScriptable<PlayerScriptableConfig>(configLoader);
            UpgradeConfigPack upgradeConfigPack = GetScriptable<UpgradeConfigPack>(configLoader);
            UpgradeModifiersConfig upgradeModifiersConfig = GetScriptable<UpgradeModifiersConfig>(configLoader);
            upgradeModifiersConfig.CostAllUpgrades = PlayerPrefs.GetInt(UPGRADE_COINS_KEY);
            metaprogression.CollectedCoins = PlayerPrefs.GetInt(COINS_KEY);
            LoadAchievmentList(achievementList);
            LoadCharachters(charachters);
            LoadUpgrades(upgradeConfigPack);
            LoadUpgradeModifiersConfig(upgradeModifiersConfig);
            LoadStatistics(globalStatisticConfig);
        }

        public void TrySave(ConfigLoader configLoader)
        {
            MetaprogressionConfig metaprogression = GetScriptable<MetaprogressionConfig>(configLoader);
            AchievementListConfig achievementList = metaprogression.AchievementList;
            GlobalStatisticConfig globalStatisticConfig = GetScriptable<GlobalStatisticConfig>(configLoader);
            PlayerScriptableConfig charachters = GetScriptable<PlayerScriptableConfig>(configLoader);
            UpgradeModifiersConfig upgradeModifiersConfig = GetScriptable<UpgradeModifiersConfig>(configLoader);
            PlayerPrefs.SetInt(UPGRADE_COINS_KEY, upgradeModifiersConfig.CostAllUpgrades);
            PlayerPrefs.SetInt(COINS_KEY, metaprogression.CollectedCoins);
            SaveAchievmentList(achievementList);
            SaveCharachters(charachters);
            SaveStatistics(globalStatisticConfig);
            PlayerPrefs.Save();
        }

        public void TrySaveUpgrades(ConfigLoader configLoader)
        {
            UpgradeConfigPack upgradeConfigPack = GetScriptable<UpgradeConfigPack>(configLoader);
            UpgradeModifiersConfig upgradeModifiersConfig = GetScriptable<UpgradeModifiersConfig>(configLoader);
            MetaprogressionConfig metaprogression = GetScriptable<MetaprogressionConfig>(configLoader);
            SaveUpgrades(upgradeConfigPack);
            SaveUpgradeModifiersConfig(upgradeModifiersConfig);
            PlayerPrefs.SetInt(COINS_KEY, metaprogression.CollectedCoins);
            PlayerPrefs.Save();
        }

        public void TrySaveSettings()
        {
            SaveAudioSettings();
            PlayerPrefs.Save();
        }

        public void TryLoadSettings()
        {
            LoadAudioSettings();
        }

        private T GetScriptable<T>(ConfigLoader configLoader) where T : ScriptableObject
        {
            return configLoader.LoadedScriptables.Find(scriptable => scriptable.GetType() == typeof(T)) as T;
        }

        private void SaveAchievmentList(AchievementListConfig achievementList)
        {
            Achievement concreteAchievment;
            for (int i = 0; i < achievementList.Achievements.Count; i++)
            {
                concreteAchievment = achievementList.Achievements[i];
                PlayerPrefs.SetString(ACHIEVEMENT_PREFIX + concreteAchievment.Name, concreteAchievment.Unlocked.ToString());
            }
        }

        private void LoadAchievmentList(AchievementListConfig achievementList)
        {
            Achievement concreteAchievment;
            for (int i = 0; i < achievementList.Achievements.Count; i++)
            {
                concreteAchievment = achievementList.Achievements[i];
                if (PlayerPrefs.HasKey(ACHIEVEMENT_PREFIX + concreteAchievment.Name))
                {
                    concreteAchievment.Unlocked = bool.Parse(PlayerPrefs.GetString(ACHIEVEMENT_PREFIX + concreteAchievment.Name));
                }
            }
        }

        private void SaveCharachters(PlayerScriptableConfig charachters)
        {
            CharacterConfig character;
            for (int i = 0; i < charachters.Characters.Length; i++)
            {
                character = charachters.Characters.GetValueByIndex(i);
                PlayerPrefs.SetString(CHARACTER_PREFIX + character.CharacterName, character.IsUnlocked.ToString());
            }
        }

        private void LoadCharachters(PlayerScriptableConfig charachters)
        {
            CharacterConfig character;
            for (int i = 0; i < charachters.Characters.Length; i++)
            {
                character = charachters.Characters.GetValueByIndex(i);
                if (PlayerPrefs.HasKey(CHARACTER_PREFIX + character.CharacterName))
                {
                    character.IsUnlocked = bool.Parse(PlayerPrefs.GetString(CHARACTER_PREFIX + character.CharacterName));
                }
            }
        }

        private void SaveStatistics(GlobalStatisticConfig statistics)
        {
            PlayerPrefs.SetInt(TOTAL_COINS_KEY, statistics.TotalCoins);
            PlayerPrefs.SetFloat(TOTAL_TIME_KEY, statistics.TotalTime);
            PlayerPrefs.SetInt(TOTAL_KILLS_KEY, statistics.TotalKills);
            PlayerPrefs.SetInt(RUN_COUNT_KEY, statistics.RunCount);

            foreach (var weapon in statistics.WeaponsData)
            {
                PlayerPrefs.SetString(STATISTICS_PREFIX + weapon.Key, weapon.Value.ToString());
            }
        }

        private void LoadStatistics(GlobalStatisticConfig statistics)
        {
            statistics.TotalCoins = PlayerPrefs.GetInt(TOTAL_COINS_KEY);
            statistics.TotalTime = PlayerPrefs.GetFloat(TOTAL_TIME_KEY);
            statistics.TotalKills = PlayerPrefs.GetInt(TOTAL_KILLS_KEY);
            statistics.RunCount = PlayerPrefs.GetInt(RUN_COUNT_KEY);

            foreach (string weaponName in statistics.WeaponsName)
            {
                if (PlayerPrefs.HasKey(STATISTICS_PREFIX + weaponName))
                {
                    if (!statistics.WeaponsData.ContainsKey(weaponName))
                    {
                        statistics.WeaponsData.Add(weaponName, int.Parse(PlayerPrefs.GetString(STATISTICS_PREFIX + weaponName)));
                    }
                    else
                    {
                        statistics.WeaponsData[weaponName] = int.Parse(PlayerPrefs.GetString(STATISTICS_PREFIX + weaponName));
                    }
                }
            }
        }

        private void SaveUpgrades(UpgradeConfigPack upgradeConfigPack)
        {
            foreach (UpgradeConfig upgradeConfig in upgradeConfigPack.UpgradeConfigs)
            {
                PlayerPrefs.SetInt(UPGRADE_PREFIX + upgradeConfig.UpgradeName, upgradeConfig.CurrentLevel);
            }
        }

        private void LoadUpgrades(UpgradeConfigPack upgradeConfigPack)
        {
            foreach (UpgradeConfig upgradeConfig in upgradeConfigPack.UpgradeConfigs)
            {
                if (PlayerPrefs.HasKey(UPGRADE_PREFIX + upgradeConfig.UpgradeName))
                {
                    upgradeConfig.CurrentLevel = PlayerPrefs.GetInt(UPGRADE_PREFIX + upgradeConfig.UpgradeName);
                }
            }
        }
        private void SaveUpgradeModifiersConfig(UpgradeModifiersConfig upgradeModifiersConfig)
        {
            PlayerPrefs.SetInt(UPRGADE_MODIFIER_KEY, upgradeModifiersConfig.CostAllUpgrades);
        }

        private void LoadUpgradeModifiersConfig(UpgradeModifiersConfig upgradeModifiersConfig)
        {
            if (PlayerPrefs.HasKey(UPRGADE_MODIFIER_KEY))
            {
                upgradeModifiersConfig.CostAllUpgrades = PlayerPrefs.GetInt(UPRGADE_MODIFIER_KEY);
            }
        }


        private void SaveAudioSettings()
        {
            AudioService audioService = AudioService.Instance;
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, audioService.GetCurrentMasterVolume());
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, audioService.GetCurrentMusicVolume());
            PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, audioService.GetCurrentEffectVolume());
        }

        private void LoadAudioSettings()
        {
            AudioService audioService = AudioService.Instance;
            if (PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
            {
                audioService.ChangeMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_KEY));
            }
            if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
            {
                audioService.ChangeMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY));
            }
            if (PlayerPrefs.HasKey(EFFECTS_VOLUME_KEY))
            {
                audioService.ChangeEffectsVolume(PlayerPrefs.GetFloat(EFFECTS_VOLUME_KEY));
            }
        }
    }
}