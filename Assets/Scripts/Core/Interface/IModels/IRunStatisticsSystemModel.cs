using System;
using System.Collections.Generic;
using MVP.Models.StatisticsSystemModels;
using Enums;

namespace Core.Interface.IModels
{
    internal interface IRunStatisticsSystemModel
    {
        public event Action<int> OnCollectedCoinsChange;
        
        public int CollectedCoins { get; set; }
        public float TotalTime { get; }
        public string LocationName { get; }
        public int TotalKills { get; }

        public Dictionary<WeaponsName, int> KilledByWeapon { get; }
        public Dictionary<WeaponCard, int> KilledByWeaponCard { get; }
        public void AddWeaponInStatistics(IWeaponModel weapon);
        public void AddLocationModel(ILocationModel location);
        public void AddTotalTime();
    }
}