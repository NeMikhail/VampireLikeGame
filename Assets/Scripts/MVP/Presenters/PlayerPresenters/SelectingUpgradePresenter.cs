using System;
using Audio;
using Audio.Service;
using Core;
using Core.ImprovmentSystem.Upgrade;
using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using MVP.Presenters.InputPresenters;
using Structs.UpgradeSystem;
using UI;

namespace MVP.Presenters.PlayerPresenters
{
    public sealed class SelectingUpgradePresenter : IInitialisation, ICleanUp
    {
        private readonly UiUpgradeScreen _ui;
        private readonly IExpirienceLevelModel _levelModel;
        private readonly UpgradeFactory _upgradeFactory;
        private readonly PausePresenter _pausePresenter;

        private readonly PlayerStatsChanger _statsChanger;
        private readonly UpgradeConverter _upgradeConverter;

        private readonly IUpgrade[] _currentUpgrades = new IUpgrade[ConstantsProvider.MAX_UPGRADES_ON_SELECT];

        public event Action OnSelectingUpgradesActivated;
        public event Action OnSelectingUpgradesDeactivated;

        internal SelectingUpgradePresenter(IViewProvider viewProvider, IDataProvider dataProvider,
            UpgradeFactory upgradeFactory, PausePresenter pausePresenter)
        {
            _ui = viewProvider.GetSelectUpgradeScreen();
            _levelModel = dataProvider.LevelModel;
            _upgradeFactory = upgradeFactory;
            _pausePresenter = pausePresenter;

            _statsChanger = new PlayerStatsChanger(dataProvider.ModifiersModel);
            _upgradeConverter = new UpgradeConverter(_statsChanger, dataProvider);

            _ui.Hide();
        }


        public void Initialisation()
        {
            _ui.OnUpgradeSelect += OnSelectUpgrade;
            _levelModel.OnLevelNumberChanged += OnLevelUp;
            OnSelectingUpgradesActivated += _pausePresenter.PauseOn;
            OnSelectingUpgradesDeactivated += _pausePresenter.PauseOff;
        }

        public void Cleanup()
        {
            _ui.OnUpgradeSelect -= OnSelectUpgrade;
            _levelModel.OnLevelNumberChanged -= OnLevelUp;
            OnSelectingUpgradesActivated -= _pausePresenter.PauseOn;
            OnSelectingUpgradesDeactivated -= _pausePresenter.PauseOff;
        }

        public void OnLevelUp(int levelNumber)
        {
            if (levelNumber == ConstantsProvider.DEFAULT_LEVEL_NUMBER)
                return;

            IUpgradeInfo[] currentUpgrades = _upgradeFactory.GenerateUpgrades();
            for (var i = 0; i < currentUpgrades.Length; i++)
            {
                _currentUpgrades[i] = _upgradeConverter.Convert(currentUpgrades[i]);
            }  

            if (currentUpgrades[0] is MoneyUpgradeInfo)
            {
                _currentUpgrades[0].Apply();
                return;
            }

            _ui.SetUpgrades(currentUpgrades);
            AudioService.Instance.PlayAudioOneShot(AudioClipNames.LevelUp_Character, true);
            Show();
        }

        public void OnSelectUpgrade(int num)
        {
            _currentUpgrades[num].Apply();

            Hide();
        }

        private void Show()
        {
            OnSelectingUpgradesActivated?.Invoke();
            _ui.Show();
        }

        private void Hide()
        {
            OnSelectingUpgradesDeactivated?.Invoke();
            _ui.Hide();
        }
    }
}