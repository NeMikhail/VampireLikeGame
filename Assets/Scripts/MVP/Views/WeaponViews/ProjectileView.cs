using System;
using UnityEngine;
using Core.Interface;
using Core.Interface.IFeatures;
using Core.Interface.IModels;

namespace MVP.Views.WeaponViews
{
    public sealed class ProjectileView : MonoBehaviour, IPoolObject<ProjectileView>
    {
        [SerializeField] private Collider _colliderObj;
        [SerializeField] private Rigidbody _rigidBodyObj;
        private IWeaponModel _weapon;

        public Vector3 Direction { get; set; }
        public int BounceCurrentCount { get; set; }
        public bool IsActive { get; set; }
        public bool isHit { get; set; }
        public float LifeTime { get; set; }
        public int DamageEnemyCount { get; set; }
        public Rigidbody RigidBodyObj { get => _rigidBodyObj;}
        public Collider ColliderObj { get => _colliderObj; }
        public IWeaponModel Weapon { get => _weapon; set => _weapon = value; }

        public event Action<ProjectileView> OnSpawnObject;
        public event Action<ProjectileView> OnDeSpawnObject;
        public event Action<IDamageable, ProjectileView> OnStartDamageEnemy;
        public event Action<IDamageable, ProjectileView> OnStopDamageEnemy;
        public event Action<IWeaponModel, ProjectileView> OnBounce;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var enemy))
                OnStartDamageEnemy?.Invoke(enemy, this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var enemy))
                OnStopDamageEnemy?.Invoke(enemy, this);
        }

        public void OnDisable() => OnSpawnObject?.Invoke(this);
        public void OnEnable() => OnDeSpawnObject?.Invoke(this);

        public void ChangeBounceCurrentCount()
        {
            BounceCurrentCount++;
            OnBounce?.Invoke(_weapon, this);
        } 
    }
}