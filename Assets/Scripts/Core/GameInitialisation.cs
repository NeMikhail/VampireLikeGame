using UnityEngine;
using Core.Interface;
using Core.Interface.IFactories;
using Core.Interface.IInputs;
using Infrastructure.InputSystem;

namespace Core
{
    internal sealed class GameInitialisation : IGameStrategy
    {
        private IAbstractGameFactory _gameFactory;
        private IInputInitialisation _inputInitialization;
        private IViewProvider _viewProvider;
        private PresentersProvider _presentersProvider;

        internal GameInitialisation(IAbstractGameFactory factory, IViewProvider viewProvider)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform ==
                RuntimePlatform.WindowsPlayer)
            {
                _inputInitialization = new KeyboardInput();
            }
            else if(Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
            {
                _inputInitialization = new JoystickModel();
            }
            else
            {
                _inputInitialization = new KeyboardInput();
            }
            _gameFactory = factory;
            _viewProvider = viewProvider;
            _presentersProvider = _gameFactory.PresentersProvider;
            GamePreInit();
        }


        public void GamePreInit()
        {
            _viewProvider.GetBattleScreen().ShowLoadingScreen();
            _gameFactory.SetInput(_inputInitialization);
            _gameFactory.CreatePlayerSystems();
            _gameFactory.CreateLocationGenerationSystem();
            _presentersProvider.LocationGeneratorPresenter.OnLocationGenerated += GameInit;
        }

        public void GameInit()
        {
            _viewProvider.GetBattleScreen().HideLoadingScreen();
            _gameFactory.CreateLocationPresenter();
            _gameFactory.CreateEnemySystems();
            _gameFactory.CreateRunStatisticsSystem();
            _gameFactory.CreateDamagePopupPresenter();
            _gameFactory.CreateWeaponSystem();            
            _gameFactory.CreatePassiveItems();
            _gameFactory.CreateLevelStatusSystem();
            _gameFactory.CreatePickingUpSystem();
            _gameFactory.CreateRadiusExperiencePickRegulator();
            _gameFactory.CreateLocationObjectsSystem();
            _gameFactory.CreatePausablePresenters();
            _gameFactory.CreateAchievementPresenter();
            _gameFactory.CreateGlobalStatisticSystem();
            _gameFactory.CreateUpgradeSystem();
            _gameFactory.CreateUISystems();
            _presentersProvider.LocationGeneratorPresenter.OnLocationGenerated -= GameInit;
        }
    }
}