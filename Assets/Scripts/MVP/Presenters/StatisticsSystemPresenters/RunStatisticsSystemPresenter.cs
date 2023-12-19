using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;
using MVP.Models.StatisticsSystemModels;

namespace MVP.Presenters.StatisticsSystemPresenters
{
    internal sealed class RunStatisticsSystemPresenter : IExecute, ICleanUp
    {
        private IDataProvider _dataProvider;
        private IPlayerModel _playerModel;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;

        public RunStatisticsSystemPresenter(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _playerModel = _dataProvider.PlayerModel;
            _runStatisticsSystemModel = new RunStatisticsSystemModel(_dataProvider);
            _dataProvider.AddRunStatisticsModel(_runStatisticsSystemModel);
            _playerModel.OnAddNewWeapon += _runStatisticsSystemModel.AddWeaponInStatistics;
        }

        public void Execute(float deltaTime)
        {
            _runStatisticsSystemModel.AddTotalTime();
        }

        public void Cleanup()
        {
            _playerModel.OnAddNewWeapon -= _runStatisticsSystemModel.AddWeaponInStatistics;
        }
    }
}