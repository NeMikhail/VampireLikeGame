using Core.EnemyService.Patterns;
using Core.Factory;
using Core.Interface;
using Core.Interface.IFactories;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;
using MVP.Views.PlayerViews;

namespace Core.EnemyService
{
    internal sealed class EnemySystem : IExecute, IFixedExecute, ICleanUp
    {
        private readonly PlayerView _playerView;
        private readonly RespownSystem _respownSystem;
        private readonly TeleportManager _teleportManager;
        private readonly RespawnQueueManager _respawnQueueManager;
        private readonly RandomSpawnPointMaker _randomSpawnPointMaker;
        private readonly IRandomUnitVectorGenerator _randomUnitVectorGenerator;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IDataFactory _dataFactory;
        private readonly EnemyUpdater _enemyUpdater;
        private readonly PatternsQueueManager _patternsQueueManager;
        private readonly IViewProvider _viewProvider;

        public EnemySystem(PlayerView playerView, IDataFactory dataFactory, PresentersProvider presentersProvider,
            IDataProvider dataProvider, IViewProvider viewProvider)
        {
            _playerView = playerView;
            _dataFactory = dataFactory;

            _randomUnitVectorGenerator = new RandomUnitVectorGenerator();
            _randomSpawnPointMaker = new RandomSpawnPointMaker(_randomUnitVectorGenerator);
            _enemyFactory = new EnemyFactory();
            _respownSystem = new RespownSystem(_enemyFactory, _dataFactory, _randomSpawnPointMaker, _playerView,
                presentersProvider, dataProvider);
            _teleportManager = new TeleportManager(_respownSystem, _randomSpawnPointMaker);
            _respawnQueueManager = new RespawnQueueManager(_respownSystem, dataProvider);
            _randomUnitVectorGenerator = new RandomUnitVectorGenerator();
            _enemyUpdater = new EnemyUpdater(_respownSystem);
            _patternsQueueManager = new PatternsQueueManager(dataProvider, _respownSystem, _randomSpawnPointMaker,
                _playerView);
            _viewProvider = viewProvider;
            _respownSystem.OnCreateBossHPBar += _viewProvider.GetBattleScreen().ShowBossHPBar;
        }


        public void Execute(float deltaTime)
        {
            _teleportManager.Execute();
            _respawnQueueManager.Execute();
            _randomUnitVectorGenerator.Execute();
            _patternsQueueManager.Execute();
        }

        public void FixedExecute(float deltaTime)
        {
            _enemyUpdater.FixedExecute(deltaTime);
        }

        public void Cleanup()
        {
            _respawnQueueManager.Cleanup();
            _respownSystem.Cleanup();
            _patternsQueueManager.Cleanup();
            _respownSystem.OnCreateBossHPBar -= _viewProvider.GetBattleScreen().ShowBossHPBar;
        }
    }
}