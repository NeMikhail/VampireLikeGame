using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using MVP.Models.PassiveItemModels;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfigs", menuName = "Configs/WeaponSystems", order = 1)]
    public sealed class WeaponScriptableObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private WeaponScriptableObjectType _type;
        [SerializeField] private List<WeaponAttributes> _items = new List<WeaponAttributes>();

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public List<WeaponAttributes> Items { get => _items; }
        public WeaponScriptableObjectType Type { get => _type; set => _type = value; }

        [Serializable]
        public sealed class WeaponAttributes
        {
            [SerializeField] private string _displayName;
            [SerializeField] private WeaponsName _name;
            [SerializeField] private string _description;
            [SerializeField] private Sprite _icon;
            [SerializeField] private WeaponScriptableObject _levelUpCfg;
            [SerializeField] private AudioClip _shotSound;
            [SerializeField] private float _probability;
            [Space]
            [Header("Weapon Synergy")]
            [SerializeField] private bool _isSynergy;
            [SerializeField] private List<PassiveItemInfo> _passiveForSynergy;
            [SerializeField] private WeaponScriptableObject _weaponSynergy;
            [Space]
            [Header("Weapon Attribute")]
            [SerializeField] private WeaponType _weaponType;
            [SerializeField] private WeaponTypeTarget _target;
            [SerializeField] private float _damage;
            [SerializeField] private float _damageReplayTime;
            [SerializeField] private float _area;
            [SerializeField] private float _chanceCritical;
            [SerializeField] private int _multiplierCritical;
            [SerializeField] private float _cooldown;
            [Space]
            [Header("Projectile Attribute")]
            [SerializeField] private GameObject _projectile;
            [SerializeField] private GameObject _hitImpact;
            [SerializeField] private float _projectileSpeed;
            [SerializeField] private float _projectileInterval;
            [SerializeField] private int _projectileMaxCount;
            [SerializeField] private float _projectileDuration;
            [SerializeField] private int _projectileMaxDamageEnemy;
            [SerializeField] private int _projectileMaxBounceCount;
            [SerializeField] private bool _isKnockback;


            public WeaponsName Name { get => _name; set => _name = value; }
            public string Description { get => _description; set => _description = value; }
            public Sprite Icon { get => _icon; set => _icon = value; }
            public float Damage { get => _damage; set => _damage = value; }
            public float DamageReplayTime { get => _damageReplayTime; set => _damageReplayTime = value; }
            public float ProjectileInterval { get => _projectileInterval; set => _projectileInterval = value; }
            public float Area { get => _area; set => _area = value; }
            public float ChanceCritical { get => _chanceCritical; set => _chanceCritical = value; }
            public int MultiplierCritical { get => _multiplierCritical; set => _multiplierCritical = value; }
            public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
            public int ProjectileMaxCount { get => _projectileMaxCount; set => _projectileMaxCount = value; }
            public float Probability { get => _probability; set => _probability = value; }
            public int ProjectileMaxBounceCount
            {
                get => _projectileMaxBounceCount;
                set => _projectileMaxBounceCount = value;
            }
            public WeaponType WeaponType { get => _weaponType; set => _weaponType = value; }
            public WeaponTypeTarget Target { get => _target; set => _target = value; }
            public float Cooldown { get => _cooldown; set => _cooldown = value; }
            public float ProjectileDuration { get => _projectileDuration; set => _projectileDuration = value; }
            public int ProjectilePiercingMaxEnemies {
                get => _projectileMaxDamageEnemy;
                set => _projectileMaxDamageEnemy = value;
            }
            public WeaponScriptableObject LevelUpCfg { get => _levelUpCfg; set => _levelUpCfg = value; }
            public string DisplayName { get => _displayName; set => _displayName = value;}
            public bool IsSynergy { get => _isSynergy; set => _isSynergy = value; }
            public List<PassiveItemInfo> PassiveForSynergy
            {
                get => _passiveForSynergy;
                set => _passiveForSynergy = value;
            }
            public WeaponScriptableObject WeaponSynergy { get => _weaponSynergy; set => _weaponSynergy = value; }
            public bool IsKnockback { get => _isKnockback; set => _isKnockback = value; }
            public AudioClip ShotSound { get => _shotSound; set => _shotSound = value; }
            public GameObject HitImpact { get => _hitImpact; set => _hitImpact = value; }
            internal GameObject Projectile { get => _projectile; set => _projectile = value; }
        }
    }
}