using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(GlobalStatisticConfig), menuName = "Configs/GlobalStatistic")]
    public sealed class GlobalStatisticConfig : ScriptableObject
    {
        [SerializeField] private float _totalTime;
        [SerializeField] private int _totalCoins;
        [SerializeField] private int _runCount;
        [SerializeField] private int _totalKills;

        private Dictionary<string, int> _weaponsData = new Dictionary<string, int>();
        private KeyValuePair<string, int> _favoriteWeapon = new KeyValuePair<string, int>();

        public List<string> WeaponsName;

        public float TotalTime { get => _totalTime; set => _totalTime = value; }
        public int TotalCoins { get => _totalCoins; set => _totalCoins = value; }
        public int RunCount { get => _runCount; set => _runCount = value; }
        public int TotalKills { get => _totalKills; set => _totalKills = value; }
        public Dictionary<string, int> WeaponsData { get => _weaponsData; set => _weaponsData = value; }
        public KeyValuePair<string, int> FavoriteWeapon 
        { 
            get
            {
                SetFavoriteWeapon();
                return _favoriteWeapon;
            }
        }

        public void SetFavoriteWeapon()
        {
            KeyValuePair<string, int> favoriteWeapon = new KeyValuePair<string, int>();
            foreach (var weapon in _weaponsData)
            {
                if (weapon.Value > favoriteWeapon.Value)
                {
                    favoriteWeapon = weapon;
                }
            }
            _favoriteWeapon = favoriteWeapon;
        }
    }
}