using System;
using Core;
using Core.Interface.IModels;
using Enums;
using MVP.Views.EnemyViews;
using Structs;

namespace MVP.Models.EnemyModels
{
    internal class EnemyModel : IEnemyModel
    {
        private float _enemySpeed;
        private float _enemyDefaultSpeed;
        private float _enemyDefaultDamage;
        private float _enemyDefaultMaxHealth;
        private float _enemyMaxHealth;
        private float _enemyHealth;
        private float _enemyDamage;
        private EnemyType _enemyType;
        private IModifiersModel _modifiersModel;
        private TypeOfExperience _typeOfExperience;

        public event Action OnDie;
        public event Action<float> OnHealthChanged;

        public float EnemySpeed
        {
            get { return _enemySpeed; }
            set { _enemySpeed = value * ConstantsProvider.GLOBAL_SPEED_MULTIPLER; }
        }
        public float EnemyMaxHealth
        {
            get { return _enemyMaxHealth; }
            set
            {
                _enemyMaxHealth = value;
            }
        }
        public float EnemyHealth
        {
            get { return _enemyHealth; }
            set
            {
                _enemyHealth = value;
                OnHealthChanged?.Invoke(value);
                if (_enemyHealth <= 0)
                {
                    OnDie?.Invoke();
                    DeInit();
                }
            }
        }
        public float EnemyDamage
        {
            get { return _enemyDamage; }
            set { _enemyDamage = value; }
        }
        public EnemyType EnemyType
        {
            get { return _enemyType; }
            set { _enemyType = value; }
        }
       
        public TypeOfExperience TypeOfExperience
        {
            get { return _typeOfExperience; }
            set { _typeOfExperience = value; }
        }

        internal EnemyModel(float enemySpeed, float enemyMaxHealth, float enemyDamage, EnemyType enemyType,
            TypeOfExperience typeOfExperience, IModifiersModel modifiersModel)
        {
            _modifiersModel = modifiersModel;
            _enemyDefaultMaxHealth = enemyMaxHealth;
            _enemyDefaultDamage = enemyDamage;
            _enemyDefaultSpeed =  enemySpeed * ConstantsProvider.GLOBAL_SPEED_MULTIPLER;
            _enemyType = enemyType;
            _typeOfExperience = typeOfExperience;
            Init();
        }


        private void Init()
        {
            _enemySpeed = _enemyDefaultSpeed * _modifiersModel.EnemySpeedMultiplier;
            _enemyMaxHealth = _enemyDefaultMaxHealth * _modifiersModel.EnemyHealthMultiplier;
            _enemyHealth = _enemyMaxHealth;
            _enemyDamage = _enemyDefaultDamage*_modifiersModel.EnemyDamageMultiplier;
            _modifiersModel.EnemyDamageMultiplierChanged += ChangeDamage;
            _modifiersModel.EnemyHealthMultiplierChanged += ChangeHealth;
            _modifiersModel.EnemySpeedMultiplierChanged += ChangeSpeed;
        }

        private void DeInit()
        {
            _modifiersModel.EnemyDamageMultiplierChanged -= ChangeDamage;
            _modifiersModel.EnemyHealthMultiplierChanged -= ChangeHealth;
            _modifiersModel.EnemySpeedMultiplierChanged -= ChangeSpeed;
        }


        private void ChangeSpeed(float multiplier)
        {
            _enemySpeed = _enemyDefaultSpeed * multiplier;
        }

        private void ChangeHealth(float multiplier)
        {
            if (_enemyMaxHealth <= 0) return;
            _enemyHealth /= _enemyMaxHealth;
            _enemyMaxHealth = _enemyDefaultMaxHealth * multiplier;
            _enemyHealth *= _enemyMaxHealth;
        }

        private void ChangeDamage(float multiplier)
        {
            _enemyDamage = _enemyDefaultDamage * multiplier;
        }
    }
}