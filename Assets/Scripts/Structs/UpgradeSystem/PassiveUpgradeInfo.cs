using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IUpgrades;
using Enums;

namespace Structs.UpgradeSystem
{
    [Serializable]
    internal struct PassiveUpgradeInfo : IPassiveUpgrade
    {
        [SerializeField] private string _caption;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [Range(0f, 1f)]
        [SerializeField] private float _probability;

        [Space(10), Header("Features")]
        [SerializeField] private List<ModifierBlock> _multiplierList;

        public string Caption { get => _caption; set => _caption = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public string Description { get => _description; set => _description = value; }
        public List<ModifierBlock> MultiplierList => _multiplierList;
        public float  Probability { get => _probability; set => _probability = value; }


        [Serializable]
        public struct ModifierBlock
        {
            public ModifierType Modifier;
            public float DeltaMultiplier;
        }
    }
}