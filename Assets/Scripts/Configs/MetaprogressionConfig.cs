using System;
using UnityEngine;
using Core.Metaprogression;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(MetaprogressionData), menuName = "MetaprogressionData")]
    public sealed class MetaprogressionConfig : ScriptableObject
    {
        [SerializeField] private int _collectedCoins;
        [SerializeField] private AchievementListConfig _achievements;
        
        public event Action<int> OnCoinsCountChange;

        public AchievementListConfig AchievementList
        {
            get => _achievements;
            set => _achievements = value;
        }
        public int CollectedCoins
        {
            get => _collectedCoins;
            set
            {
                _collectedCoins = value;
                OnCoinsCountChange?.Invoke(value);
            }
        }
    }
}