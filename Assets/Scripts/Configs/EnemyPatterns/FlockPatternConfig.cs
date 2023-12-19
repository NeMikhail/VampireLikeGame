using UnityEngine;
using Core.EnemyService.Patterns.PatternVariants;
using Enums;

namespace Configs.EnemyPatterns
{
    [CreateAssetMenu(fileName = nameof(FlockPatternConfig), menuName = "Configs/Enemy/Patterns/" +
        nameof(FlockPatternConfig))]
    internal sealed class FlockPatternConfig : AbstractPatternConfig
    {
        [SerializeField]
        public int EnemyCount;

        [SerializeField]
        public EnemyType enemyType;


        public override AbstractEnemyPattern GetPattern()
        {
            return new FlockPattern(enemyType, EnemyCount);
        }
    }
}