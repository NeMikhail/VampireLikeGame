using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.CollectablePresenters
{
    internal sealed class CoinCollectPresenter : IInitialisation, ICleanUp
    {
        private ICoinsView _coinsView;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;

        internal CoinCollectPresenter(IDataProvider dataProvider, IViewProvider viewProvider)
        {
            _coinsView = viewProvider.GetCoinsRunView();
            _runStatisticsSystemModel = dataProvider.RunStatisticsModel;
        }


        public void Initialisation()
        {
            _runStatisticsSystemModel.OnCollectedCoinsChange += ChangeCoinsValue;
            ChangeCoinsValue(0);
        }

        public void Cleanup()
        {
            _runStatisticsSystemModel.OnCollectedCoinsChange -= ChangeCoinsValue;
        }

        private void ChangeCoinsValue(int value)
        {
            _coinsView.SetCoins(value);
        }
    }
}