using System;
using Configs;
using Core.Interface.IModels;

namespace MVP.Models.LocationObjectModel
{
    public sealed class LocationObjectModel : ILocationObjectModel
    {
        private bool _isBreakable;
        private float _maxHealth;
        private float _health;

        public event Action OnBreaking;
        
        public float MaxHealth => _maxHealth;
        public float Health => _health;

        public LocationObjectModel(LocationObjectConfig config)
        {
            _isBreakable = config.IsBreakable;
            _maxHealth = config.MaxHealth;
            _health = config.Health;
        }       


        public void TakeDamage(float damage)
        {
            if (!_isBreakable) return;

            _health -= damage;
            if(_health <= 0)
            {
                Break();           
            }         
        }

        private void Break()
        {
            OnBreaking?.Invoke();
        }
    }
}