using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;

namespace MVP.Presenters.UIPresenters
{
    internal class UITimerPresenter : IFixedExecute
    {
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private ITimerView _timerView;

        public UITimerPresenter(IViewProvider viewProvider, IDataProvider dataProvider) 
        {
            _runStatisticsSystemModel = dataProvider.RunStatisticsModel;
            _timerView = viewProvider.GetTimerView();
        }


        public void FixedExecute(float fixedDeltaTime)
        {
            float totalTime = _runStatisticsSystemModel.TotalTime;
            _timerView.SetTime((int)totalTime);
        }
    }
}