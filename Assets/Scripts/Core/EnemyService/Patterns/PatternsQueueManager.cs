using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Core.EnemyService.Patterns.PatternVariants;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using MVP.Views.PlayerViews;

namespace Core.EnemyService.Patterns
{
    internal sealed class PatternsQueueManager
    {
        const float STEP = 0.01f;

        private SortedList<float, AbstractEnemyPattern> _patternQueue = new SortedList<float, AbstractEnemyPattern>();
        private IEnumerator<KeyValuePair<float, AbstractEnemyPattern>> _enumerator;
        private List<IExecutablePattern> _executablePatterns;

        private float _gameTime;
        private IDataProvider _dataProvider;
        private RespownSystem _respownSystem;
        private RandomSpawnPointMaker _randomSpawnPointMaker;
        private PlayerView _playerView;
        private bool _IsHasNext = true;

        public PatternsQueueManager(IDataProvider dataProvider, RespownSystem respownSystem,
            RandomSpawnPointMaker randomSpawnPointMaker, PlayerView playerView)
        {
            _gameTime = 0f;
            _dataProvider = dataProvider;
            _respownSystem=respownSystem;
            _randomSpawnPointMaker = randomSpawnPointMaker;
            _playerView = playerView;
            Init();
        }


        public void Execute()
        {
            _gameTime += Time.deltaTime;
            CheckQueue();
            ExecutePatterns();
        }

        private void Init()
        {
            _executablePatterns = new List<IExecutablePattern>();
            
            RespownModel respownConfig = ResourceLoadManager.GetConfig<LocationsPack>().CurrentLocation.EnemyRespawnModel;
            EnemyPatternsList enemyPatternList = ResourceLoadManager.GetConfig<EnemyPatternsList>();

            for (int i = 0; i < respownConfig.EnemyPatterns.Count; i++)
            {
                RespownModel.EnemyPattern patternConfig = respownConfig.EnemyPatterns[i];

                float startTime = patternConfig.StartTimeInMinuts * 60;
                while (_patternQueue.ContainsKey(startTime))
                {
                    startTime += STEP;
                }

                AbstractEnemyPattern pattern = enemyPatternList.GetConfig(patternConfig.EnemyType).GetPattern();
                _patternQueue.Add(startTime, pattern);
                pattern.OnInit(_respownSystem, _randomSpawnPointMaker, _playerView, _dataProvider);
            }

            _enumerator = _patternQueue.GetEnumerator();

            if (!_enumerator.MoveNext())
                _IsHasNext = false;
        }

        private void OffList(IExecutablePattern sender)
        {
            sender.OnComplet -= OffList;
            _executablePatterns.Remove(sender);

        }

        private void ExecutePatterns()
        {
            int length = _executablePatterns.Count;
            for(int i = 0; i < length; i++)
            {
                _executablePatterns[i].OnExecute();
            }
        }

        private void CheckQueue()
        {
            if (_IsHasNext && _enumerator.Current.Key <= _gameTime)
            {
                AbstractEnemyPattern pattern = _enumerator.Current.Value;
                pattern.OnStart();

                if (pattern is IExecutablePattern executablePattern)
                {
                    _executablePatterns.Add(executablePattern);
                    executablePattern.OnComplet += OffList;
                }

                if (!_enumerator.MoveNext())
                    _IsHasNext = false;
            }
        }

        public void Cleanup()
        {
            while(_executablePatterns.Count > 0)
                OffList(_executablePatterns[0]);
            
            _patternQueue.Clear();
        }
    }
}