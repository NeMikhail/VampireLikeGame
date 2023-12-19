using UnityEngine;
using MVP.Views.LocationObjectViews;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(LocationObjectConfig), menuName = "Configs/LocationObjectConfig")]
    public sealed class LocationObjectConfig : ScriptableObject
    {
        [SerializeField] private LocationObjectView _locationObjectPrefab;
        [SerializeField] private bool _isBreakable;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;

        public LocationObjectView LocationObjectPrefab => _locationObjectPrefab;
        public bool IsBreakable => _isBreakable;
        public float MaxHealth => _maxHealth;
        public float Health => _health;
    }
}