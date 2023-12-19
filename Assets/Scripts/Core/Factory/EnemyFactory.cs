using Core.Interface.IFactories;
using Core.Interface.IFeatures;
using Infrastructure.PoolSystems.Pool;
using UnityEngine;

namespace Core.Factory
{
    internal class EnemyFactory : IEnemyFactory
    {
        public IRespawnable Respawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            IRespawnable enemy = GamePool.Enemy.Pool.Spawn(prefab);
            enemy.RespawnableTransform.position = position;
            enemy.RespawnableTransform.rotation = rotation;
            return enemy;
        }
    }
}