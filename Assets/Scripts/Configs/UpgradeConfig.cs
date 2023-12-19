using UnityEngine;
using Enums;
using UnityEngine.Serialization;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(UpgradeConfig), menuName = "Configs/Upgrade/" + nameof(UpgradeConfig))]
    internal sealed class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private string _upgradeName;
        [SerializeField] private ModifierType _modifierType;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _maxLevel;
        [SerializeField] private Sprite _upgradeImage;
        [SerializeField] private float _additionModifierPerLevel;
        [SerializeField] private float _upgradeCostMultiplier;
        [FormerlySerializedAs("_upgradeCost")]
        [SerializeField] private int _baseUpgradeCost;
        private int _upgradeCost;

        public string UpgradeName => _upgradeName;
        public ModifierType ModifierType => _modifierType;
        public int CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }
        public int MaxLevel
        {
            get { return _maxLevel; }
            set { _maxLevel = value; }
        }
        public Sprite UpgradeImage => _upgradeImage;
        public float AdditionModifierPerLevel
        {
            get { return _additionModifierPerLevel; }
            set { _additionModifierPerLevel = value; }
        }
        public int UpgradeCost
        {
            set { _upgradeCost = value; }
            get { return _upgradeCost; }
        }
        public float UpgradeCostMultiplier
        {
            set { _upgradeCostMultiplier = value / 100f; }
            get { return _upgradeCostMultiplier / 100f; }
        }
        public int BaseUpgradeCost
        {
            set { _baseUpgradeCost = value; }
            get { return _baseUpgradeCost; }
        }
    }
}