using UnityEngine;
using Core.EnemyService.Patterns.PatternVariants;
using Enums;

namespace Configs.EnemyPatterns
{
    [CreateAssetMenu(fileName = nameof(NonTeleportStandingAroundConfig), menuName = "Configs/Enemy/Patterns/" +
        nameof(NonTeleportStandingAroundConfig))]
    internal sealed class NonTeleportStandingAroundConfig : AbstractPatternConfig
    {
        [SerializeField]
        public int EnemyCount;

        [SerializeField]
        public EnemyType enemyType;


        public override AbstractEnemyPattern GetPattern()
        {
            return new NonTeleportableStandingAround(enemyType, EnemyCount);
        }
    }
}