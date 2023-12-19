using UnityEngine;
using Core.EnemyService.Patterns.PatternVariants;
using Enums;

namespace Configs.EnemyPatterns
{
    [CreateAssetMenu(fileName = nameof(EnemiesStandingAroundPatternConfig), menuName = "Configs/Enemy/Patterns/" +
        nameof(EnemiesStandingAroundPatternConfig))]
    internal sealed class EnemiesStandingAroundPatternConfig : AbstractPatternConfig
    {
        [SerializeField]
        public int EnemyCount;

        [SerializeField]
        public EnemyType enemyType;


        public override AbstractEnemyPattern GetPattern()
        {
            return new EnemiesStandingAroundPattern(enemyType, EnemyCount);
        }
    }
}