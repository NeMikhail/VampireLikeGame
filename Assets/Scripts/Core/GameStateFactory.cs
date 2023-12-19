using UnityEngine;
using Configs;
using Core.Animations.Player;
using Core.EnemyService;
using Core.ImprovmentSystem.Upgrade;
using Core.Interface;
using Core.Interface.IFactories;
using Core.Interface.IInputs;
using Core.Interface.IPresenters;
using Core.LocationObjectsSystem;
using Core.ResourceLoader;
using MVP.Presenters.ExperiencePresenters;
using MVP.Presenters.InputPresenters;
using MVP.Presenters.LevelPresenter;
using MVP.Presenters.PlayerPresenters;
using MVP.Presenters.RadiusExperiencePickPresenters;
using MVP.Presenters.WeaponPresenters;
using MVP.Presenters.GameState;
using MVP.Presenters.LocationPresenter;
using MVP.Presenters.LocationObjectPresenter;
using MVP.Presenters.StatisticsSystemPresenters;
using MVP.Presenters.UIPresenters;
using MVP.Presenters.CollectablePresenters;
using MVP.Presenters.PassiveItemsUpgradePresenter;
using MVP.Presenters.AchievementPresenter;
using MVP.Presenters.WorldGenerator;
using MVP.Presenters.PickingUpExperienceVisualizators;
using MVP.Views.PlayerViews;
using MVP.Views.LocationObjectViews;

namespace Core
{
    internal sealed class GameStateFactory : IAbstractGameFactory
    {
        private IInputInitialisation _inputInitialisation;
        private InputPresenter _inputPresenter;
        private readonly IDataProvider _dataProvider;
        private readonly IViewProvider _viewProvider;
        private readonly PresentersProvider _presentersProvider;
        private readonly IDataFactory _dataFactory;
        private readonly Presenters _presenters;
        private PlayerView _playerView;
        private UpgradeFactory _upgradeFactory;
        private PausePresenter _pausePresenter;
        private HealthChangingPresenter _healthChangingPresenter;
        private DamagePopupPresenter _damagePopupPresenter;

        public PresentersProvider PresentersProvider => _presentersProvider;

        internal GameStateFactory(Presenters presenters, IDataProvider dataProvider, IViewProvider viewProvider,
            IDataFactory dataFactory)
        {
            _presenters = presenters;
            _dataProvider = dataProvider;
            _viewProvider = viewProvider;
            _dataFactory = dataFactory;
            _upgradeFactory = new UpgradeFactory(_dataProvider);
            _presentersProvider = new PresentersProvider();
        }


        public void SetInput(IInputInitialisation inputInitialisation)
        {
            _inputInitialisation = inputInitialisation;
            _inputPresenter = new InputPresenter(_inputInitialisation);
            _presenters.Add(_inputPresenter);
        }

        public void CreateLocationGenerationSystem()
        {
            var locationGenerator = new LocationGeneratorPresenter();
            _presenters.Add(locationGenerator);
            PresentersProvider.LocationGeneratorPresenter = locationGenerator;
        }
        
        public void CreatePlayerSystems()
        {
            var startPosition = new Vector3(0, 0, 0);
            var rotation = new Quaternion();

            GameObject playerObject = GameObject.Instantiate(ResourceLoadManager.GetConfig<PlayerScriptableConfig>().
                PlayerPrefab, startPosition, rotation);
            Transform playerTransform = playerObject.transform;
            var playerRigidbody = playerObject.GetComponent<Rigidbody>();

            _playerView = playerObject.GetOrAddComponent<PlayerView>();
            GameObject playerObjectView = _playerView.PlayerObject;
            _playerView.SetPlayerViewData(playerObjectView, playerTransform, playerRigidbody);
            _healthChangingPresenter = new HealthChangingPresenter(_playerView, _dataProvider.PlayerModel, _viewProvider);
            var playerAnimation = new PlayerAnimation(_playerView.PlayerAnimator);
            var movementPresenter = new MovementPresenter(_playerView, _dataProvider.PlayerModel, _inputPresenter,
                playerAnimation);
            var playerCollisionHandler = new PlayerCollisionHandler(_playerView, _healthChangingPresenter);

            _presenters.Add(playerCollisionHandler);
            _presenters.Add(_healthChangingPresenter);
            _presenters.Add(movementPresenter);
        }

        public void CreateLocationPresenter()
        {
            _presenters.Add(new LocationPresenter(_dataProvider));
        }

        public void CreateLevelStatusSystem()
        {
            _presenters.Add(new LevelPresenter(_viewProvider, _dataProvider.LevelModel));
        }

        public void CreateRadiusExperiencePickRegulator()
        {
            _presenters.Add(new RadiusExperiencePickPresenter(_dataProvider.RadiusExperiencePickModel, _playerView, _dataProvider.PlayerModel));
        }

        public void CreateEnemySystems()
        {
            var enemySystem = new EnemySystem(_playerView, _dataFactory, _presentersProvider,
                _dataProvider, _viewProvider);
            _presenters.Add(enemySystem);
        }

        public void CreateDamagePopupPresenter()
        {
            _damagePopupPresenter = new DamagePopupPresenter(_viewProvider);
            _presenters.Add(_damagePopupPresenter);
        }

        public void CreateWeaponSystem()
        {
            _dataFactory.CreateWeaponModel(ResourceLoadManager.GetConfig<WeaponScriptableObject>());
            _presenters.Add(new WeaponPresenter(_dataProvider, _playerView, _healthChangingPresenter, _viewProvider,
                _damagePopupPresenter));
        }

        
        
        public void CreatePickingUpSystem()
        {
            _presenters.Add(new PickUpExperienceVisualize(_playerView));
            _presenters.Add(new PickupExperiencePresenter(_playerView.DetectorPlayerCollision, _dataProvider.LevelModel,
                _presentersProvider.DropExperiencePresenter));

            var analyzer = new CollectableAnalyzer(_dataProvider, _viewProvider, _upgradeFactory);
            var coinCollectPresenter = new CoinCollectPresenter(_dataProvider, _viewProvider);

            _presenters.Add(analyzer);
            _presenters.Add(coinCollectPresenter);
            _presenters.Add(new CollectablePickingUpPresenter(_playerView, analyzer));
        }

        public void CreatePassiveItems()
        {
            _dataFactory.CreatePassiveItemModel(ResourceLoadManager.GetConfig<PassiveItemsScriptableObject>());
        }

        public void CreateLocationObjectsSystem()
        {
            LocationObjectView view = ResourceLoadManager.GetConfig<LocationObjectConfig>().LocationObjectPrefab;
            var spawner = new LocationObjectSpawner(view);
            _presenters.Add(new LocationObjectsPresenter(spawner, _dataFactory));
        }

        public void CreatePausablePresenters()
        {
            _pausePresenter = new PausePresenter(_viewProvider, _inputInitialisation);
            _presenters.Add(_pausePresenter);
            var endGamePresenter = new EndGamePresenter(_viewProvider, _dataProvider, _pausePresenter);
            _presentersProvider.EndGamePresenter = endGamePresenter;
            _presenters.Add(endGamePresenter);
        }

        public void CreateAchievementPresenter()
        {
            var achievementPresenter = new AchievementPresenter(_dataProvider, _presentersProvider);
            _presenters.Add(achievementPresenter);
        }

        public void CreateRunStatisticsSystem()
        {
            _presenters.Add(new RunStatisticsSystemPresenter(_dataProvider));
        }

        public void CreateGlobalStatisticSystem()
        {
            _presenters.Add(new GlobalStatisticsPresenter(_dataProvider, PresentersProvider));
        }

        public void CreateUISystems()
        {
            var uiTimerPresenter = new UITimerPresenter(_viewProvider, _dataProvider);
            _presenters.Add(uiTimerPresenter);

            var uiItemsPresenter = new UIItemsPresenter(_dataProvider, _viewProvider);
            _presenters.Add(uiItemsPresenter);
        }

        public void CreateUpgradeSystem()
        { 
            var selectingUpgradePresenter = new SelectingUpgradePresenter(_viewProvider, _dataProvider, _upgradeFactory,
                _pausePresenter);
            _presenters.Add(selectingUpgradePresenter);

            var modifiers = new ModifiersPresenter(_dataProvider.ModifiersModel, _dataProvider.PlayerModel,
                _dataProvider.WeaponModelsList);
            _presenters.Add(modifiers);

            var upgradeWeaponPresenter = new UpgradeWeaponPresenter(_dataProvider.PlayerModel);
            _presenters.Add(upgradeWeaponPresenter);

            var upgradePassiveItemsPresenter = new PassiveItemUpgradePresenter(_dataProvider.PlayerModel);
            _presenters.Add(upgradePassiveItemsPresenter);
            
            var upgradeSynergyPresenter = new SynergyListUpdatePresenter(_dataProvider);
            _presenters.Add(upgradeSynergyPresenter);
        }
    }
}