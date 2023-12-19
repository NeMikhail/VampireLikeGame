using Core.Interface.IFeatures;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EnemyService
{
    internal sealed class TeleportManager
    {
        const float DELTA_RADIUS = 1f;

        private readonly RandomSpawnPointMaker _randomSpawnPointMaker;
        private readonly List<IRespawnable> _activeEnemies;
        private readonly float _sqrMaxRadius;

        public TeleportManager(RespownSystem respownSystem,RandomSpawnPointMaker randomSpawnPointMaker)
        {
            _randomSpawnPointMaker = randomSpawnPointMaker;
            _activeEnemies = respownSystem.TeleportableActiveEnenies;
            _sqrMaxRadius = Mathf.Pow(randomSpawnPointMaker.radiusOfSpawn + DELTA_RADIUS,2f);
        }


        public void Execute()
        {
            int length = _activeEnemies.Count;
            for(int i=0;i<length;i++)
            {
                var radiusVector = _activeEnemies[i].RespawnableTransform.position - _randomSpawnPointMaker.Center;
                if (radiusVector.sqrMagnitude > _sqrMaxRadius)
                    _activeEnemies[i].RespawnableTransform.position = _randomSpawnPointMaker.Center-radiusVector;
            }
        }
    }
}