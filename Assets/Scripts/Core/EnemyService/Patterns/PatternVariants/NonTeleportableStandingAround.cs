using UnityEngine;
using Enums;

namespace Core.EnemyService.Patterns.PatternVariants
{
    internal sealed class NonTeleportableStandingAround : AbstractEnemyPattern
    {
        private EnemyType _enemyType;
        private int _count;

        public NonTeleportableStandingAround(EnemyType enemyType, int count)
        {
            _enemyType = enemyType;
            _count = count;
        }


        public override void OnStart()
        {
            _count = (int)((float)_count * _dataProvider.ModifiersModel.EnemyCountMultiplier);
            Vector3 spawnPosition = Vector3.forward * _randomSpawnPointMaker.radiusOfSpawn;
            float angularDistance = 360 / (float)_count;
            var rotation = Quaternion.AngleAxis(angularDistance, Vector3.up);

            for (int i = 0; i < _count; i++)
            {
                _respownSystem.RespownWithoutTeleport(_enemyType, _playerView.transform.position + spawnPosition,
                    Quaternion.identity);
                spawnPosition = rotation * spawnPosition;
            }
        }
    }
}