using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Core.Interface.IFactories;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Enums;
using MVP.Models.EnemyModels;
using MVP.Presenters.ChestPresenters;
using MVP.Presenters.ExperiencePresenters;
using MVP.Views.PlayerViews;
using MVP.Presenters.GameState;

namespace Core.EnemyService
{
    internal sealed class RespownSystem
    {
        public readonly List<IRespawnable> ActiveEnemies;
        public readonly List<IRespawnable> TeleportableActiveEnenies;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IDataFactory _dataFactory;
        private readonly RandomSpawnPointMaker _randomSpawnPointMaker;
        private readonly PlayerView _playerView;
        private readonly DropExperiencePresenter _dropExperiencePresenter;
        private readonly DropChestPresenter _dropChestPresenter;
        private readonly EnemyList _enemyList;
        private readonly IModifiersModel _modifiersModel;
        private  EndGamePresenter _endGamePresenter;
        public event Action OnFinalBossSpawned;
        public event Action<IRespawnable> OnCreateBossHPBar;

        public RespownSystem(IEnemyFactory enemyFactory, IDataFactory dataFactory, RandomSpawnPointMaker randomSpawnPointMaker,
            PlayerView playerView, PresentersProvider presentersProvider, IDataProvider dataProvider)
        {
            ActiveEnemies = new List<IRespawnable>();
            TeleportableActiveEnenies = new List<IRespawnable>();
            _enemyFactory = enemyFactory;
            _dataFactory = dataFactory;
            _randomSpawnPointMaker = randomSpawnPointMaker;
            _playerView = playerView;
            _dropExperiencePresenter = presentersProvider.DropExperiencePresenter;
            _dropChestPresenter = presentersProvider.DropChestPresenter;
            _modifiersModel = dataProvider.ModifiersModel;
            presentersProvider.OnEndGamePresenterSet += SetEndGamePreseter;
            _enemyList = ResourceLoadManager.GetConfig<EnemyList>();
        }


        public void Respown(EnemyType enemyType)
        {
            Respown(enemyType, _randomSpawnPointMaker.GetPoint(), Quaternion.identity);
        }

        public void Respown(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            IRespawnable enemy = RespownWithoutTeleport(enemyType, position, rotation);
            if(enemy!=null)
                TeleportableActiveEnenies.Add(enemy);
        }

        public IRespawnable RespownWithoutTeleport(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            IRespawnable enemy = Instantiate(enemyType, position, rotation);

            if (enemy != null)
            {
                ActiveEnemies.Add(enemy);
                enemy.OnDie += OffList;
                enemy.OnRemove += OffList;
                enemy.OnDie += _dropExperiencePresenter.DropExperience;

                if (enemyType == EnemyType.Boss || enemyType == EnemyType.BossLocation1 || 
                    enemyType == EnemyType.BossLocation2 || enemyType == EnemyType.BossLocation3)
                {
                    enemy.OnDie += _dropChestPresenter.DropChest;
                }
                else if (enemyType ==  EnemyType.FinalBoss || enemyType == EnemyType.FinalBossLocation1 ||
                    enemyType == EnemyType.FinalBossLocation2 || enemyType == EnemyType.FinalBossLocation3)
                {
                    enemy.OnDie += _endGamePresenter.FinalBossDead;
                    OnFinalBossSpawned?.Invoke();
                    OnCreateBossHPBar?.Invoke(enemy);
                }
            }

            return enemy;
        }

        private void SetEndGamePreseter(PresentersProvider presentersProvider)
        {
            _endGamePresenter = presentersProvider.EndGamePresenter;
            presentersProvider.OnEndGamePresenterSet -= SetEndGamePreseter;
        }

        private IRespawnable Instantiate(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            GameObject prefab = _enemyList.GetPrefab(enemyType);
            IRespawnable enemy = _enemyFactory.Respawn(prefab, position, rotation);

            EnemyScriptableObject enemyScriptableObject = _enemyList.GetEnemyScriptableObject(enemyType);
            EnemyModel enemyModel = _dataFactory.CreateEnemyModel(enemyScriptableObject, _modifiersModel);
            enemy.Init(_playerView, enemyModel);

            return enemy;
        }

        public void Cleanup()
        {
            while (ActiveEnemies.Count > 0)
                OffList(ActiveEnemies[0]);
        }

        private void OffList(IRespawnable sender)
        {
            sender.OnDie -= OffList;
            sender.OnDie -= _dropExperiencePresenter.DropExperience;
            sender.OnDie -= _dropChestPresenter.DropChest;
            ActiveEnemies.Remove(sender);
            TeleportableActiveEnenies.Remove(sender);
            sender.OnRemove -= OffList;
            sender.OnDie -= _endGamePresenter.FinalBossDead;
        }
    }
}