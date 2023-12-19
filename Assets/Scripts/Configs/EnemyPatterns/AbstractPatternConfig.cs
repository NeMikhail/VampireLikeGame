using UnityEngine;
using Core.EnemyService.Patterns.PatternVariants;

namespace Configs.EnemyPatterns
{
    internal abstract class AbstractPatternConfig : ScriptableObject
    {
        abstract public AbstractEnemyPattern GetPattern();
    }
}