using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyExplosionConfig), menuName = "Configs/Enemy/" + nameof(EnemyExplosionConfig))]
    internal sealed class EnemyExplosionConfig : ScriptableObject
    {
        public float Distance;
        public float ExplosionRadius;
        public float Timer;
        public float Damage;
    }
}