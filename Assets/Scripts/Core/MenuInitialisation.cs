using Core.Interface;
using MVP.Presenters.AchievementPresenter;
using MVP.Presenters.UIPresenters;

namespace Core
{
    internal sealed class MenuInitialisation
    {
        private IViewProvider _viewProvider;
        private Presenters _presenters;

        public MenuInitialisation(IViewProvider viewProvider, Presenters presenters)
        {
            _viewProvider = viewProvider;
            _presenters = presenters;

            Init();
        }


        private void Init()
        {
            CreateSettingsPresenter();
            CreateAchievementUnlockPresenter();
        }

        private void CreateSettingsPresenter()
        {
            var uiSettingsPresenter = new UISettingsPresenter(_viewProvider);
            _presenters.Add(uiSettingsPresenter);
        }

        private void CreateAchievementUnlockPresenter()
        {
            var achievementUnlockPresenter = new AchievementUnlockPresenter();
            _presenters.Add(achievementUnlockPresenter);
        }
    }
}