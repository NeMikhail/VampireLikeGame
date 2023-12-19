using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using MVP.Models.PassiveItemModels;

namespace Configs
{
    [CreateAssetMenu(fileName = "PassiveItemConfigs", menuName = "Configs/PassiveItems", order = 1)]
    public sealed class PassiveItemsScriptableObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private List<PassiveItemAttributes> _items = new List<PassiveItemAttributes>();

        public string Name { get => _name; }
        public string Description { get => _description; }
        public List<PassiveItemAttributes> Items { get => _items; }


        [Serializable]
        public sealed class PassiveItemAttributes
        {
            [SerializeField] private string _displayName;
            [SerializeField] private PassiveItemsName _name;
            [SerializeField] private Sprite _icon;
            [SerializeField] private string _description;
            [SerializeField] private List<PassiveBlock> _modifiers;
            [SerializeField] private PassiveItemsScriptableObject _levelUpCfg;
            [Range(0f, 1f)]
            [SerializeField] private float _probability;

            public string DisplayName { get => _displayName; }
            public PassiveItemsName Name { get => _name; }
            public Sprite Icon { get => _icon; }
            public string Description { get => _description; }
            public PassiveItemsScriptableObject LevelUpCfg { get => _levelUpCfg; }
            public List<PassiveBlock> Modifiers { get => _modifiers; }
            public float Probability { get => _probability; }
        }
    }
}