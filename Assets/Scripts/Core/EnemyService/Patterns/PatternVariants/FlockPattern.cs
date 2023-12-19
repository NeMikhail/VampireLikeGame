using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IFeatures;
using Enums;
using MVP.Views.EnemyViews;

namespace Core.EnemyService.Patterns.PatternVariants
{
    internal class FlockPattern : AbstractEnemyPattern, IExecutablePattern
    {
        private const float SPREAD = 0.001f;
        private EnemyType _enemyType;
        private int _count;
        private List<EnemyRunForward> _activeEnemyes;
        private float _sqrMaxRadius;

        public event Action<IExecutablePattern> OnComplet;

        public FlockPattern(EnemyType enemyType, int count)
        {
            _enemyType = enemyType;
            _count = count;
            _activeEnemyes = new List<EnemyRunForward>();
        }

        public override void OnStart()
        {
            _sqrMaxRadius = Mathf.Pow(_randomSpawnPointMaker.radiusOfSpawn, 2f);

            _count = (int)((float)_count * _dataProvider.ModifiersModel.EnemyCountMultiplier);
            Vector3 spawnPosition = _randomSpawnPointMaker.GetPoint();

            for (int i = 0; i < _count; i++)
            {
                EnemyRunForward enemy = (EnemyRunForward)_respownSystem.RespownWithoutTeleport(_enemyType, spawnPosition,
                    Quaternion.identity);
                _activeEnemyes.Add(enemy);
                enemy.OnDie += OffList;
                spawnPosition += (Vector3)UnityEngine.Random.insideUnitSphere * SPREAD;
            }
        }

        private void OffList(IRespawnable obj)
        {
            obj.OnDie -= OffList;
            _activeEnemyes.Remove((EnemyRunForward)obj);
            if (_activeEnemyes.Count <= 0)
                OnComplet?.Invoke(this);
        }

        public void OnExecute()
        {
            int length = _activeEnemyes.Count;
            for (int i = 0; i < length; i++)
            {
                if ((_activeEnemyes[i].RespawnableTransform.position - _randomSpawnPointMaker.Center).sqrMagnitude <
                    _sqrMaxRadius)
                    return;
            }
            SpawnNewEnemies();
        }
        private void SpawnNewEnemies()
        {
            Vector3 newPosition = _randomSpawnPointMaker.GetPoint();
            Vector3 directionLine = _playerView.transform.position - newPosition;

            List<EnemyRunForward> previousEnemies = new List<EnemyRunForward>(_activeEnemyes);
            _activeEnemyes.Clear();

            for (int i = 0; i < _count; i++)
            {
                EnemyRunForward enemy = (EnemyRunForward)_respownSystem.RespownWithoutTeleport(_enemyType, newPosition, Quaternion.identity);
                _activeEnemyes.Add(enemy);
                enemy.OnDie += OffList;
                newPosition += (Vector3)UnityEngine.Random.insideUnitSphere * SPREAD;
                enemy.SetDirection(directionLine);
            }

            foreach (EnemyRunForward enemy in previousEnemies)
            {
                enemy.Remove();
            }
            previousEnemies.Clear();
        }
    }
}