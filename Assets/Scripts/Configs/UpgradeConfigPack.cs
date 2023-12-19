using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(UpgradeConfigPack), menuName = "Configs/Upgrade/" +
        nameof(UpgradeConfigPack))]
    internal sealed class UpgradeConfigPack : ScriptableObject
    {
        [SerializeField] private UpgradeConfig[] _upgradeConfigs;

        public UpgradeConfig[] UpgradeConfigs => _upgradeConfigs;
    }
}