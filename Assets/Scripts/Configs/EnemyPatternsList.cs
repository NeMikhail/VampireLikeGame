using System;
using System.Collections.Generic;
using Configs.EnemyPatterns;
using UnityEngine;
using Enums;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyPatternsList), menuName = "Configs/Enemy/" + nameof(EnemyPatternsList))]
    internal sealed class EnemyPatternsList : ScriptableObject
    {
        [Serializable]
        public sealed class EnemyPattern
        {
            public EnemyPatternType PatternType;
            public AbstractPatternConfig PatternConfig;
        }

        public List<EnemyPattern> PatternsListConfigs = new List<EnemyPattern>();


        public AbstractPatternConfig GetConfig(EnemyPatternType patternType)
        {
            int length = PatternsListConfigs.Count;

            for (int i = 0; i < length; i++)
            {
                if (PatternsListConfigs[i].PatternType == patternType)
                {
                    return PatternsListConfigs[i].PatternConfig;
                }
            }

            throw new Exception("pattern type \"" + patternType + "\" not found in EnemyPatternList");
        }
    }
}