using UI;

namespace Core.Interface
{
    internal interface IViewProvider
    {
        public UiMainMenuScreen GetMenuScreen();
        public UiBattleScreen GetBattleScreen();
        public ICharacterLevelView GetCharacterLevelView();
        public IExperienceView GetExperienceIndicator();
        public UiUpgradeScreen GetSelectUpgradeScreen();
        public ITimerView GetTimerView();
        public ICoinsView GetCoinsRunView();
        public ICoinsView GetCoinsMenuView();
        public IKillCouterView GetKillCouterView();
        public IItemsView GetItemsView();
        public UIStatisticView GetStatistiсView();
    }
}