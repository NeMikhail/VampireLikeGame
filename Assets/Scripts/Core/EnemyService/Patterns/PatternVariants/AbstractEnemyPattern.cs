using Core.Interface.IPresenters;
using MVP.Views.PlayerViews;

namespace Core.EnemyService.Patterns.PatternVariants
{
    internal abstract class AbstractEnemyPattern
    {
        protected RespownSystem _respownSystem;
        protected RandomSpawnPointMaker _randomSpawnPointMaker;
        protected PlayerView _playerView;
        protected IDataProvider _dataProvider;


        public void OnInit(RespownSystem respownSystem, RandomSpawnPointMaker randomSpawnPointMaker, PlayerView playerView,
            IDataProvider dataProvider)
        {
            _respownSystem = respownSystem;
            _randomSpawnPointMaker = randomSpawnPointMaker;
            _playerView = playerView;
            _dataProvider = dataProvider;
        }

        virtual public void OnStart() 
        {

        }
    }
}