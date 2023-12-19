using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using static Configs.PassiveItemsScriptableObject;
using Core.Interface.IModels;
using Enums;
using MVP.Models.WeaponModels;

namespace MVP.Models.PassiveItemModels
{
    [Serializable]
    public sealed class PassiveBlock
    {
        [SerializeField] private ModifierType _modifier;
        [SerializeField] private float _deltaMultiplier;

        public ModifierType Modifier
        {
            get => _modifier;
            set => _modifier = value;
        }
        public float DeltaMultiplier
        {
            get => _deltaMultiplier;
            set => _deltaMultiplier = value;
        }
    }

    [Serializable]
    public struct PassiveItemInfo
    {
        [SerializeField] private PassiveItemsName _name;
        [SerializeField] private int _level;

        public PassiveItemsName Name
        {
            get => _name;
            set => _name = value;
        }
        public int Level
        {
            get => _level;
            set => _level = value;
        }
    }

    internal sealed class PassiveItemModel : IPassiveItemModel
    {
        private bool _isActive;

        private PassiveItemsName _name;
        private Sprite _icon;
        private string _displayName;
        private string _description;
        private int _level;
        private int _maxLevel;
        private List<PassiveBlock> _modifiers;
        private PassiveItemsScriptableObject _levelUpCfg;
        private float _probability;

        public PassiveItemsName Name => _name;
        public string DisplayName => _displayName;
        public bool IsActive => _isActive;
        public string Description => _description;
        public int Level => _level;
        public List<PassiveBlock> Modifiers => _modifiers;
        public Sprite Icon => _icon;
        public PassiveItemsScriptableObject LevelUpCfg => _levelUpCfg;
        public int MaxLevel => _maxLevel;
        public float Probability => _probability;

        public event Action OnLevelUp;

        public PassiveItemModel(PassiveItemAttributes cfg)
        {
            _name = cfg.Name;
            _displayName = cfg.DisplayName;
            _description = cfg.Description;
            _level = 1;
            _modifiers = cfg.Modifiers;
            _maxLevel = cfg.LevelUpCfg.Items.Count + 1;
            _levelUpCfg = cfg.LevelUpCfg;
            _icon = cfg.Icon;
            _probability = cfg.Probability;
        }


        public void LevelUp()
        {
            if (_level >= _maxLevel)
                return;
            
            _level++;
            PassiveItemAttributes cfg = _levelUpCfg.Items[_level - 2];

            for (int i = 0; i < cfg.Modifiers.Count; i++)
            {
                ChangeModifier(cfg.Modifiers[i]);
            }
        }

        private void ChangeModifier(PassiveBlock block)
        {
            if (block.Modifier == ModifierType.None)
                return;

            for (int i = 0; i < _modifiers.Count; i++)
            {
                if (_modifiers[i].Modifier == block.Modifier)
                {
                    _modifiers[i].DeltaMultiplier = block.DeltaMultiplier;
                    return;
                }
            }
            _modifiers.Add(block);
            OnLevelUp?.Invoke();
        }

        public bool LevelUpInfo(out ItemInfo info)
        {
            info = new ItemInfo();

            if (_level >= _levelUpCfg.Items.Count + 1)
            {
                return false;
            }

            PassiveItemAttributes cfg = _levelUpCfg.Items[_level - 1];
            info.Name = cfg.DisplayName;
            info.Description = cfg.Description;
            info.Icon = cfg.Icon;
            return true;
        }
    }
}