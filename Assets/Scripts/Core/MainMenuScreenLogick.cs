using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Configs;
using Core.Interface;
using Core.ResourceLoader;
using Core.SaveClass;
using Core.SaveClass.SaveModules;
using Enums;
using UI;
using Unity.Services.Core;
using Unity.Services.Analytics;
using Infrastructure.Analytics;

namespace Core
{
    internal sealed class MainMenuScreenLogick
    {
        private const int UPGRADE_LEVEL_UP = 1;

        private ViewProvider _viewProvider;

        private UiMainMenuScreen _mainMenuScreen;
        private ICoinsView _coinsView;
        private MetaprogressionConfig _metaprogressionConfig;

        private UiStageSelectLogick _stageSelectLogick;

        private UICharacterSelect _characterSelect;
        private UICharacterBuyPanelView _characterBuyPanelView;

        private UIModifiersSelect _modifiersSelect;
        private UIMenuUpgradeScreen _menuUpgradeScreen;
        private UpgradeConfig _upgradeConfig;
        private UIMenuUpgradeButton _upgradeButton;
        private bool _isSomethingBought;

        public MainMenuScreenLogick(ViewProvider viewProvider)
        {
            _viewProvider = viewProvider;

            _mainMenuScreen = _viewProvider.GetMenuScreen();
            _mainMenuScreen.OnConfirmClearButtonClick += ClearConfirmButtonClick;
            _mainMenuScreen.OnStartGameButtonClick += StartGameClick;
            _mainMenuScreen.OnAnalyticsButtonClick += StartAnalyticsClick;

            _coinsView = _viewProvider.GetCoinsMenuView();

            _metaprogressionConfig = ResourceLoadManager.GetConfig<MetaprogressionConfig>();

            _stageSelectLogick = new UiStageSelectLogick(_mainMenuScreen.StageSelectContent);
            _stageSelectLogick.CreateLocationList();

            _characterSelect = new UICharacterSelect(_mainMenuScreen.CharacterSelectContent);
            _characterSelect.CreateCharacterList();
            _characterSelect.OnCharacterPanelSelected += SelectCharacter;
            _characterBuyPanelView = _mainMenuScreen.CharacterBuyPanelView;
            _characterBuyPanelView.OnBuyButtonClick += BuyCharacter;

            _modifiersSelect = new UIModifiersSelect(_mainMenuScreen.ModifiersContent);
            _modifiersSelect.CreateUpgradeButtonList();
            _modifiersSelect.OnUpgradeButtonSelected += SelectUpgradeButton;
            _menuUpgradeScreen = _mainMenuScreen.UIMenuUpgradeScreen;
            _menuUpgradeScreen.OnBuyButtonClick += BuyUpgrade;
            _menuUpgradeScreen.OnResetButtonClick += ResetUpgrade;
            _isSomethingBought = false;

            Init();
        }


        private void Init()
        {
            UpdateUpgradeLevelVisual();
            _metaprogressionConfig.OnCoinsCountChange += _coinsView.SetCoins;
            _coinsView.SetCoins(_metaprogressionConfig.CollectedCoins);
            _characterBuyPanelView.Hide();
        }

        private void ClearConfirmButtonClick()
        {
            PlayerScriptableConfig playerConfig = ResourceLoadManager.GetConfig<PlayerScriptableConfig>();
            AchievementListConfig achievementConfig = ResourceLoadManager.GetConfig<AchievementListConfig>();
            ResetUpgrade();
            PlayerPrefs.DeleteAll();
            Saver.Load(new PlayerPrefsParserModule());
            _coinsView.SetCoins(_metaprogressionConfig.CollectedCoins);
            playerConfig.LockCharacters();
            achievementConfig.LockAchievements();

#if UNITY_WEBGL

            Application.ExternalEval("document.location.reload(true)");

#endif

#if UNITY_EDITOR

            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;

#endif
        }

        private void StartGameClick()
        {
            string selectedLocation = _stageSelectLogick.GetSelectedLocation();

            if (!string.IsNullOrEmpty(selectedLocation))
            {
                SceneManager.LoadScene(selectedLocation);
            }
            else
            {
                Debug.Log("MainMenuScreenLogick->StartGameClick: location not selected or not exist.");
            }
        }

        private async void StartAnalyticsClick()
        {
            if (!UnityAnalyticsService.IsInitialized)
            {
                await UnityServices.InitializeAsync();
                AnalyticsService.Instance.StartDataCollection();
                UnityAnalyticsService.IsInitialized = true;
            }
        }

        private void SelectCharacter(CharacterName name, CharacterConfig character)
        {
            if (character.IsUnlocked)
            {
                _characterBuyPanelView.Hide();
                _characterSelect.PlayersConfig.CurrentCharacterName = name;
            }
            else
            {
                _characterBuyPanelView.Show();
                _characterBuyPanelView.CharacterCost = character.CharacterCost.ToString();
                _characterBuyPanelView.Character = character;
                _characterBuyPanelView.CharacterName = name;
                _characterSelect.PlayersConfig.CurrentCharacterName = CharacterName.None;
            }
        }

        private void BuyCharacter()
        {
            int characterCost = _characterBuyPanelView.Character.CharacterCost;

            if (characterCost <= _metaprogressionConfig.CollectedCoins)
            {
                _metaprogressionConfig.CollectedCoins -= characterCost;
                _characterBuyPanelView.Character.IsUnlocked = true;
                _characterBuyPanelView.Hide();
                _characterSelect.PlayersConfig.CurrentCharacterName = _characterBuyPanelView.CharacterName;
                Saver.Save(new PlayerPrefsParserModule());
            }
        }

        private void SelectUpgradeButton(UpgradeConfig upgradeConfig, UIMenuUpgradeButton upgradeButton)
        {
            _upgradeConfig = upgradeConfig;
            _upgradeButton = upgradeButton;
            _upgradeConfig.UpgradeCost = (int)(_upgradeConfig.BaseUpgradeCost + (_upgradeConfig.UpgradeCostMultiplier * (_upgradeConfig.CurrentLevel - 1) * _upgradeConfig.BaseUpgradeCost));
            _menuUpgradeScreen.UpgradeCost = _upgradeConfig.UpgradeCost.ToString();
            if (_upgradeConfig.CurrentLevel == _upgradeConfig.MaxLevel)
            {
                _menuUpgradeScreen.UpgradeCost = "Max";
            }
        }

        private void UpdateUpgradeLevelVisual()
        {
            UpgradeConfigPack upgradeConfigPack = ResourceLoadManager.GetConfig<UpgradeConfigPack>();
            UpgradeConfig[] upgradeConfigs = upgradeConfigPack.UpgradeConfigs;
            for (int i = 0; i < upgradeConfigs.Length; i++)
            {
                if (_modifiersSelect.UpgradeButtons[i].CurrentLevel != upgradeConfigs[i].CurrentLevel.ToString())
                {
                    _modifiersSelect.UpgradeButtons[i].CurrentLevel = upgradeConfigs[i].CurrentLevel.ToString();
                }
                _isSomethingBought = true;
            }
        }

        private void BuyUpgrade()
        {
            if (_upgradeConfig == null)
                return;

            int upgradeCost = _upgradeConfig.UpgradeCost;

            if (upgradeCost <= _metaprogressionConfig.CollectedCoins && _upgradeConfig.CurrentLevel < _upgradeConfig.MaxLevel)
            {
                new UIBuyUpgrade(_upgradeConfig);
                _metaprogressionConfig.CollectedCoins -= upgradeCost;
                _upgradeConfig.CurrentLevel += UPGRADE_LEVEL_UP;
                _upgradeButton.CurrentLevel = _upgradeConfig.CurrentLevel.ToString();
                _upgradeConfig.UpgradeCost = (int)(_upgradeConfig.BaseUpgradeCost + (_upgradeConfig.UpgradeCostMultiplier * (_upgradeConfig.CurrentLevel - 1) * _upgradeConfig.BaseUpgradeCost));
                if (_upgradeConfig.CurrentLevel == _upgradeConfig.MaxLevel)
                {
                    _menuUpgradeScreen.UpgradeCost = "Max";
                }
                else
                {
                    _menuUpgradeScreen.UpgradeCost = _upgradeConfig.UpgradeCost.ToString();
                }
                Saver.SaveUpgrades(new PlayerPrefsParserModule());
                _isSomethingBought = true;
            }
        }

        private void ResetUpgrade()
        {
            if (_isSomethingBought)
            {
                var uiResetUpgrades = new UIResetUpgrades();
                _metaprogressionConfig.CollectedCoins += uiResetUpgrades.CostAllUpgrades;
                uiResetUpgrades.ResetUpgrades();
                _modifiersSelect.ResetUpgradeButtonList();
                if (_upgradeConfig != null)
                {
                    _menuUpgradeScreen.UpgradeCost = _upgradeConfig.UpgradeCost.ToString();
                }
                Saver.SaveUpgrades(new PlayerPrefsParserModule());
            }
        }

        public void Cleanup()
        {
            _mainMenuScreen.OnConfirmClearButtonClick -= ClearConfirmButtonClick;
            _mainMenuScreen.OnStartGameButtonClick -= StartGameClick;
            _mainMenuScreen.OnAnalyticsButtonClick -= StartAnalyticsClick;
            _metaprogressionConfig.OnCoinsCountChange -= _coinsView.SetCoins;

            _stageSelectLogick.Cleanup();

            _characterSelect.Cleanup();
            _characterSelect.OnCharacterPanelSelected -= SelectCharacter;
            _characterBuyPanelView.OnBuyButtonClick -= BuyCharacter;

            _modifiersSelect.Cleanup();
            _modifiersSelect.OnUpgradeButtonSelected -= SelectUpgradeButton;
            _menuUpgradeScreen.OnBuyButtonClick -= BuyUpgrade;
            _menuUpgradeScreen.OnResetButtonClick -= ResetUpgrade;
        }
    }
}