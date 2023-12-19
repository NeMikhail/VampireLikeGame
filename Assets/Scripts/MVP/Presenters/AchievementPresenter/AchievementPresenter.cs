using Core;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using MVP.Models.AchievementModel;
using MVP.Presenters.GameState;

namespace MVP.Presenters.AchievementPresenter
{
    internal sealed class AchievementPresenter : IInitialisation, ICleanUp
    {
        private IDataProvider _dataProvider;
        private PresentersProvider _presentersProvider;
        private EndGamePresenter _endGamePresenter;
        private IAchievementModel _achievementModel;

        public AchievementPresenter(IDataProvider dataProvider, PresentersProvider presentersProvider)
        {
            _dataProvider = dataProvider;
            _endGamePresenter = presentersProvider.EndGamePresenter;
        }


        public void Initialisation()
        {
            _endGamePresenter.OnGameEnded += CheckAchievements;
            _achievementModel = new AchievementModel(_dataProvider);
            _dataProvider.AddAchievementModel(_achievementModel);
            _achievementModel.RegisterAchievements();
        }

        public void CheckAchievements()
        {
            _achievementModel.CheckAchievementState();
        }

        public void Cleanup()
        {
            _endGamePresenter.OnGameEnded -= CheckAchievements;
            _achievementModel.Cleanup();
        }
    }
}