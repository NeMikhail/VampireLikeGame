using UnityEngine;
using Core.EnemyService.Patterns.PatternVariants;
using Enums;

namespace Configs.EnemyPatterns
{
    [CreateAssetMenu(fileName = nameof(FinallyBossPatternConfig), menuName = "Configs/Enemy/Patterns/" +
        nameof(FinallyBossPatternConfig))]
    internal sealed class FinallyBossPatternConfig : AbstractPatternConfig
    {
        [SerializeField]
        public EnemyType enemyType;

        public override AbstractEnemyPattern GetPattern()
        {
            return new FinalliBossPattern(enemyType);
        }
    }
}