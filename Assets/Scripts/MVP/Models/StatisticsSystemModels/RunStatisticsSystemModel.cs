using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Enums;

namespace MVP.Models.StatisticsSystemModels
{
    public struct WeaponCard
    {
        public string Name;
        public Sprite Image;
    }

    internal sealed class RunStatisticsSystemModel : IRunStatisticsSystemModel
    {
        private int _collectedCoins;
        private float _totalTime;
        private ILocationModel _locationModel;
        private Dictionary<WeaponsName, int> _killedByWeapon;
        private Dictionary<WeaponCard, int> _killedByWeaponCard;

        public event Action<int> OnCollectedCoinsChange;

        public int CollectedCoins
        {
            get => _collectedCoins;
            set
            {
                _collectedCoins = value;
                OnCollectedCoinsChange?.Invoke(value);
            }
        }
        public float TotalTime { get => _totalTime; }
        public string LocationName { get => _locationModel.CurrentLocation.LocationName; }
        public int TotalKills
        {
            get
            {
                int total = 0;
                foreach (KeyValuePair<WeaponsName, int> killed in _killedByWeapon)
                {
                    total += killed.Value;
                }
                return total;
            }
        }
        public Dictionary<WeaponsName, int> KilledByWeapon { get => _killedByWeapon; }
        public Dictionary<WeaponCard, int> KilledByWeaponCard { get => _killedByWeaponCard; }

        public RunStatisticsSystemModel(IDataProvider dataProvider)
        {
            _locationModel = dataProvider.LocationModel;
            _killedByWeapon = new Dictionary<WeaponsName, int>();
            _killedByWeaponCard = new Dictionary<WeaponCard, int>();
        }


        public void AddWeaponInStatistics(IWeaponModel weapon)
        {
            WeaponCard card;
            card.Name = weapon.DisplayName;
            card.Image = weapon.Icon;
            if (!_killedByWeapon.ContainsKey(weapon.Name))
            {
                _killedByWeapon.Add(weapon.Name, 0);
            }
            else
            {
                _killedByWeapon[weapon.Name]++;
            }
            if (!_killedByWeaponCard.ContainsKey(card))
            {
                _killedByWeaponCard.Add(card, 0);
            }
            else
            {
                _killedByWeaponCard[card]++;
            }
        }

        public void AddLocationModel(ILocationModel location)
        {
            _locationModel = location;
        }

        public void AddTotalTime()
        {
            _totalTime += Time.deltaTime;
        }
    }
}