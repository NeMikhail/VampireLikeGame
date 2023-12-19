using UnityEngine;
using Enums;
using Structs;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy/EnemyModel", order = 1)]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float EnemySpeed;
        public float EnemyHealth;
        public float EnemyDamage;
        public EnemyType EnemyType;
        public TypeOfExperience TypeOfExperience;
    }
}