using Core.Interface.IInputs;

namespace Core.Interface.IFactories
{
    internal interface IAbstractGameFactory
    {
        public void SetInput(IInputInitialisation inputInitialisation);
        public void CreateLocationGenerationSystem();
        public void CreatePlayerSystems();
        public void CreateLocationPresenter();
        public void CreateEnemySystems();
        public void CreateDamagePopupPresenter();
        public void CreateWeaponSystem();
        public void CreateRunStatisticsSystem();
        public void CreatePassiveItems();
        public void CreateLevelStatusSystem();
        public void CreatePickingUpSystem();
        public void CreateRadiusExperiencePickRegulator();       
        public void CreateLocationObjectsSystem();
        public void CreatePausablePresenters();
        public void CreateAchievementPresenter();
        public void CreateGlobalStatisticSystem();
        public void CreateUISystems();
		public void CreateUpgradeSystem();

        public PresentersProvider PresentersProvider { get; }
    }
}