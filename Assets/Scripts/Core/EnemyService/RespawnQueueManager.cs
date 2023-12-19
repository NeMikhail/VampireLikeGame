using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Core.Interface.IPresenters;
using Core.ResourceLoader;

namespace Core.EnemyService
{
    internal sealed class RespawnQueueManager
    {
        const float STEP = 0.01f;

        private SortedList<float, EnemyWave> RespawnQueue = new SortedList<float, EnemyWave>();
        private IEnumerator<KeyValuePair<float,EnemyWave>> _enumerator;

        private RespownSystem _respownSystem;
        private float _gameTime;
        private IDataProvider _dataProvider;
        private bool _IsHasNext=true;

        public RespawnQueueManager(RespownSystem respownSystem, IDataProvider dataProvider)
        {
            _gameTime = 0f;
            _respownSystem = respownSystem;
            _dataProvider = dataProvider;
            Init();
        }


        public void Execute()
        {
            _gameTime += Time.deltaTime;
            CheckQueue();
        }

        private void Init()
        {
            RespownModel config = ResourceLoadManager.GetConfig<LocationsPack>().CurrentLocation.EnemyRespawnModel;
           
            for (int i = 0; i < config.Waves.Count; i++)
            {
                RespownModel.WaveOfAttacks wave = config.Waves[i];
                float startTime = wave.StartTimeInMinuts * 60;
                float totalTime = (wave.EndTimeInMinuts - wave.StartTimeInMinuts) * 60;
                float delta = totalTime / wave.EnemyCount;

                while (RespawnQueue.ContainsKey(startTime)) 
                    startTime += STEP;

                RespawnQueue.Add(startTime, new EnemyWave(delta, _respownSystem, _dataProvider.ModifiersModel, totalTime,
                    wave.EnemyType));
            }

            _enumerator = RespawnQueue.GetEnumerator();

            if (!_enumerator.MoveNext())
                _IsHasNext = false;
        }

        private void CheckQueue()
        {
            if(_IsHasNext && _enumerator.Current.Key <= _gameTime)
            {
                _enumerator.Current.Value.StartRespown();
                if (!_enumerator.MoveNext())
                    _IsHasNext = false;
            }
        }

        public void Cleanup()
        {
            foreach (EnemyWave wave in RespawnQueue.Values)
                wave.StopRespown();

            RespawnQueue.Clear();
        }
    }
}