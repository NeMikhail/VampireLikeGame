using MVP.Models.EnemyModels;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyBulletModel), menuName = "Configs/Enemy/" + nameof(EnemyBulletModel))]
    internal class EnemyBulletConfig : ScriptableObject
    {
        public float Interval;
        public float Speed;
        public GameObject Prefab;
    }
}