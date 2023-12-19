using Core.Interface;
using UI;

namespace Core
{
    internal sealed class ViewProvider : IViewProvider
    {
        private ViewFactory _viewFactory;
        private UiBattleScreen _battleScreen;
        private UiMainMenuScreen _mainMenuScreen;
        private UiUpgradeScreen _upgradeScreen;
        private IExperienceView _experienceView;
        private ICharacterLevelView _characterLevelView;
        private ITimerView _timerView;
        private ICoinsView _coinsRunView;
        private ICoinsView _coinsMenuView;
        private IKillCouterView _killCouter;
        private IItemsView _itemsView;
        private UIStatisticView _statistiсView;
        private bool _haveBattleScreen;

        public ViewProvider()
        {
            _viewFactory = new ViewFactory();
            _viewFactory.CreateMainCanvas();
        }


        public UiBattleScreen GetBattleScreen()
        {
            if (!_haveBattleScreen)
            {
                _battleScreen = _viewFactory.CreateBattleScreen();
                _experienceView = _battleScreen.GetIExperienceIndicator();
                _characterLevelView = _battleScreen.GetCharacterLevelIndicator();
                _timerView = _battleScreen.GetTimerView();
                _coinsRunView = _battleScreen.GetCoinsView();
                _killCouter = _battleScreen.GetKillCouter();
                _itemsView = _battleScreen.GetItemsView();
                _statistiсView = _battleScreen.GetStatistiсView();
                _haveBattleScreen = true;
            }
            return _battleScreen;
        }

        public UiMainMenuScreen GetMenuScreen()
        {
            if (_mainMenuScreen == null)
            {
                _mainMenuScreen = _viewFactory.CreateMainMenuScreen();
                _coinsMenuView = _mainMenuScreen.CoinsView;
            }
            return _mainMenuScreen;
        }

        public IExperienceView GetExperienceIndicator()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _experienceView;
        }

        public ICharacterLevelView GetCharacterLevelView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _characterLevelView;
        }

        public ITimerView GetTimerView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _timerView;
        }

        public ICoinsView GetCoinsRunView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _coinsRunView;
        }

        public ICoinsView GetCoinsMenuView()
        {
            return _coinsMenuView;
        }

        public IKillCouterView GetKillCouterView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _killCouter;
        }

        public IItemsView GetItemsView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _itemsView;
        }

        public UIStatisticView GetStatistiсView()
        {
            if (!_haveBattleScreen)
            {
                GetBattleScreen();
            }
            return _statistiсView;
        }

        public UiUpgradeScreen GetSelectUpgradeScreen()
        {
            if (_upgradeScreen == null)
            {
                _upgradeScreen = _viewFactory.CreateUpgradeScreen();
            }
            return _upgradeScreen;
        }
    }
}