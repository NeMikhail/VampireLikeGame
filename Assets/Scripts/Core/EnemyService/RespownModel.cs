using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Core.EnemyService
{
    [CreateAssetMenu(fileName = nameof(RespownModel), menuName = "Configs/" + nameof(RespownModel))]
    internal sealed class RespownModel : ScriptableObject
    {
        [Serializable]
        public sealed class WaveOfAttacks
        {
            public EnemyType EnemyType;
            public float StartTimeInMinuts;
            public float EndTimeInMinuts;
            public int EnemyCount;
        }

        [Serializable]
        public sealed class EnemyPattern
        {
            public EnemyPatternType EnemyType;
            public float StartTimeInMinuts;
        }

        public List<WaveOfAttacks> Waves = new List<WaveOfAttacks>();
        public List<EnemyPattern> EnemyPatterns = new List<EnemyPattern>();
    }
}