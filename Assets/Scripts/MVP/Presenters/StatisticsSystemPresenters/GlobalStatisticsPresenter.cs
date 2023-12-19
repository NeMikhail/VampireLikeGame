using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Configs;
using Core.ResourceLoader;
using Core;
using MVP.Presenters.GameState;
using System.Collections.Generic;
using Infrastructure.Analytics;
using UnityEngine;

namespace MVP.Presenters.StatisticsSystemPresenters
{
    internal class GlobalStatisticsPresenter : ICleanUp, IInitialisation
    {
        private GlobalStatisticConfig _globalStatisticConfig;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private EndGamePresenter _endGamePresenter;

        public GlobalStatisticsPresenter(IDataProvider dataProvider, PresentersProvider presentersProvider)
        {
            _globalStatisticConfig = ResourceLoadManager.GetConfig<GlobalStatisticConfig>();
            _runStatisticsSystemModel = dataProvider.RunStatisticsModel;
            _endGamePresenter = presentersProvider.EndGamePresenter;
        }

        public void Initialisation()
        {
            _endGamePresenter.OnGameEnded += SetEndgameStatisctics;
        }

        public void SetEndgameStatisctics()
        {
            _globalStatisticConfig.RunCount++;
            _globalStatisticConfig.TotalCoins += _runStatisticsSystemModel.CollectedCoins;
            _globalStatisticConfig.TotalKills += _runStatisticsSystemModel.TotalKills;
            _globalStatisticConfig.TotalTime += _runStatisticsSystemModel.TotalTime;
            var weaponsData = _globalStatisticConfig.WeaponsData;
            var weaponsRun = _runStatisticsSystemModel.KilledByWeaponCard;

            foreach (var weapon in weaponsRun)
            {
                if (!weaponsData.ContainsKey(weapon.Key.Name))
                {
                    weaponsData.Add(weapon.Key.Name, weapon.Value);
                } else
                {
                    weaponsData[weapon.Key.Name] += weapon.Value;
                }
            }

            SendAnalyticEvent();
        }

        public void SendAnalyticEvent()
        {
            var favoriteWeapon = _globalStatisticConfig.FavoriteWeapon;
            Dictionary<string, object> statisticData = new Dictionary<string, object>()
            {
                ["RunCount"] = _globalStatisticConfig.RunCount,
                ["TotalCoins"] = _globalStatisticConfig.TotalCoins,
                ["TotalTime"] = _globalStatisticConfig.TotalTime,
                ["TotalKills"] = _globalStatisticConfig.TotalKills,
                ["FavoriteWeapon"] = favoriteWeapon.Key,
                ["FavoriteWeaponKills"] = favoriteWeapon.Value

            };

            UnityAnalyticsService.SendEvent("GameRunStatistics", statisticData);
        }

        public void Cleanup()
        {
            _endGamePresenter.OnGameEnded -= SetEndgameStatisctics;
        }

        
    }
}