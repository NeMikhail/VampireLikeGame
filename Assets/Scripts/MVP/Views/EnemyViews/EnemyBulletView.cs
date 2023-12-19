using System;
using Core.Interface;
using Core.Interface.IFeatures;
using Infrastructure.TimeRemaningService;
using UnityEngine;

namespace MVP.Views.EnemyViews
{
    public class EnemyBulletView : MonoBehaviour, IPoolObject<EnemyBulletView>, IAttackable
    {
        public bool IsActive { get; set; }

        public event Action<EnemyBulletView> OnSpawnObject;

        public event Action<EnemyBulletView> OnDeSpawnObject;
        
        public float Damage
        {
            get
            {
                gameObject.SetActive(false);
                return 1;
            }
        }

        private void Awake()
        {
            _timeRemaining = new TimeRemaining(OnEnableBullet, 2.0f);
        }

        private TimeRemaining _timeRemaining;

        public void OnEnable()
        {
            OnSpawnObject?.Invoke(this);
            _timeRemaining.AddTimeRemaining();
        }

        public void OnDisable()
        {
            OnDeSpawnObject?.Invoke(this);
            _timeRemaining.RemoveTimeRemaining();
        }

        private void OnEnableBullet()
        {
            gameObject.SetActive(false);
        }
    }
}