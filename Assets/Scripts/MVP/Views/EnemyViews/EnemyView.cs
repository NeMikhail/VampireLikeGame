using System;
using UnityEngine;
using Core.Animations.Enemy;
using Core.Interface;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Core.Interface.IViews;
using MVP.Models.EnemyModels;
using MVP.Presenters.EnemyPresenters;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;
using Structs;
using Enums;

namespace MVP.Views.EnemyViews
{
    internal class EnemyView : MonoBehaviour, IEnemyView, IPoolObject<EnemyView>, IRespawnable, IAttackable, IDamageable
    {
        [SerializeField] private GameObject _enemyObject;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private bool _isCamVisible;
        [SerializeField] protected Rigidbody _enemyRigidbody;
        [SerializeField] protected Animator _enemyAnimator;
        protected EnemyModel _enemyModel;
        protected EnemyMovement _enemyMovement;

        private Collider _collider;

        public bool IsActive { get; set; }

        public event Action<IRespawnable> OnDie;
        public event Action<IRespawnable> OnRemove;
        public event Action<IDamageable> OnStop;
        public event Action<EnemyView> OnSpawnObject;
        public event Action<EnemyView> OnDeSpawnObject;

        public bool IsCamVisible
        {
            get => _isCamVisible;
        }

        public EnemyModel EnemyModel
        {
            get => _enemyModel;
        }

        public Transform Transform
        {
            get => _enemyTransform;
        }
        public GameObject EnemyObject
        {
            get { return _enemyObject; }
            set { _enemyObject = value; }
        }
        public Transform EnemyTransform
        {
            get { return _enemyTransform; }
            set { _enemyTransform = value; }
        }
        public float Damage
        {
            get
            {
                //here will be the logic of animation
                return _enemyModel.EnemyDamage;
            }
        }
        public Animator EnemyAnimator
        {
            get => _enemyAnimator;
            set => _enemyAnimator = value;
        }
        public Transform RespawnableTransform => _enemyTransform;
        public TypeOfExperience TypeOfExperience
        {
            get { return _enemyModel.TypeOfExperience; }
        }


        public void OnEnable()
        {
            OnSpawnObject?.Invoke(this);
        }

        public virtual void OnDisable()
        {
            OnStop?.Invoke(this);
            OnDeSpawnObject?.Invoke(this);
        }

        public virtual void Init(PlayerView player, EnemyModel enemyModel)
        {
            _enemyModel = enemyModel;
            if (_enemyAnimator != null)
            {
                var enemyAnimation = new EnemyAnimation(_enemyAnimator);
                _enemyMovement = new EnemyMovement(player, this, _enemyModel, _enemyRigidbody, enemyAnimation);
            }
            else
            {
                _enemyMovement = new EnemyMovement(player, this, _enemyModel, _enemyRigidbody, null);
            }
            _enemyModel.OnDie += Dead;
            _collider = GetComponent<Collider>();
        }

        public void FixedExecute(float deltaTime)
        {
            _enemyMovement.FixedExecute(deltaTime);
        }

        public void CauseDamage(float damage)
        {
            _enemyModel.EnemyHealth -= damage;
        }

        public void CauseKnockback(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            _enemyMovement.CauseKnockBack(projectile, weapon.ProjectileSpeed);
            weapon.OnStartDamageEnemy -= CauseKnockback;
        }

        protected void Dead()
        {
            _enemyModel.OnDie -= Dead;
            OnDie?.Invoke(this);
            gameObject.SetActive(false);
        }

        public virtual void Remove()
        {
            _enemyModel.OnDie -= Dead;
            OnRemove?.Invoke(this);
            gameObject.SetActive(false);
        }

        private void CheckForCollisions()
        {
            if (_enemyModel.EnemyType == EnemyType.FlyingForFlockPattern || 
                _enemyModel.EnemyType == EnemyType.Flying ||
                _enemyModel.EnemyType == EnemyType.FinalBoss)
                return;

            _collider = GetComponent<Collider>();
            float radiusReducer = 0.2f;
            float colliderRadius = _collider.bounds.size.x / 2;
            Vector3 position = 
                new Vector3(transform.position.x, transform.position.y + colliderRadius, transform.position.z);

            Collider[] collisions = Physics.OverlapSphere(position, colliderRadius - radiusReducer);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (!collisions[i].GetComponent<EnemyView>())
                    Remove();
            }
        }

        private void OnBecameVisible()
        {
            CheckForCollisions();
            _isCamVisible = true;
        }

        private void OnBecameInvisible()
        {
            _isCamVisible = false;
        }
    }
}