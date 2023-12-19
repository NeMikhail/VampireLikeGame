using Core.Interface.IModels;
using Enums;
using Infrastructure.TimeRemaningService;

namespace Core.EnemyService
{
    internal sealed class EnemyWave
    {
        private readonly float _defaultDelta;
        private TimeRemaining _respownTimer;
        private TimeRemaining _stopRespawnTimer;

        private readonly RespownSystem _respownSystem;
        private readonly EnemyType _enemyType;
        private readonly IModifiersModel _modifiersModel;
        private readonly float _totalTime;

        public EnemyWave(float delta, RespownSystem respownSystem, IModifiersModel modifiersModel, float totalTimeOfSpawn,
            EnemyType enemyType)
        {
            _modifiersModel = modifiersModel;
            _defaultDelta = delta;
            _respownSystem = respownSystem;
            _totalTime = totalTimeOfSpawn;
            _enemyType = enemyType;
            respownSystem.OnFinalBossSpawned += StopRespown;
        }


        private void ModifEnemyCount(float modif)
        {
            _respownTimer.RemoveTimeRemaining();
            _respownTimer = new TimeRemaining(Respown, _defaultDelta / _modifiersModel.EnemyCountMultiplier, true);
            _respownTimer.AddTimeRemaining();
        }

        private void Respown()
        {
            _respownSystem.Respown(_enemyType);
        }

        public void StartRespown()
        {
            _respownTimer = new TimeRemaining(Respown, _defaultDelta / _modifiersModel.EnemyCountMultiplier, true);
            _respownTimer.AddTimeRemaining();

            _stopRespawnTimer = new TimeRemaining(StopRespown, _totalTime, false);
            _stopRespawnTimer.AddTimeRemaining();

            _modifiersModel.EnemyCountMultiplierChanged += ModifEnemyCount;
        }

        public void StopRespown()
        {
            _respownTimer.RemoveTimeRemaining();
            _stopRespawnTimer.RemoveTimeRemaining();
            _modifiersModel.EnemyCountMultiplierChanged -= ModifEnemyCount;
        }
    }
}