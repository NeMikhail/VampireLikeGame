using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Interface;
using Core.Interface.IFeatures;
using Structs.UpgradeSystem;

namespace UI
{
    internal sealed class UiBattleScreen : UiBaseScreen
    {
        [SerializeField] private Button _mobilePauseButton;
        [SerializeField] private GameObject _pauseMenuPanel;
        [SerializeField] private Button _quinButtonInPause;
        [SerializeField] private Button _continueButton;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _quinButtonGameOver;
        [SerializeField] private TMP_Text _gameOverText;
        [SerializeField] private RectTransform _expiriensBarFiller;
        [SerializeField] private Text _characterLevel;
        [SerializeField] private Text _timeIndicator;
        [SerializeField] private TMP_Text _coinIndicator;
        [SerializeField] private Text _killCounterText;
        [SerializeField] private UIChestPanelView _chestPanelView;
        [SerializeField] private Image[] _weaponCells;
        [SerializeField] private Image[] _passiveItemCells;

        [SerializeField] private GameObject _statisticRunPanel;
        [SerializeField] private TMP_Text _globalStatText;
        [SerializeField] private GameObject _prefabWeaponCard;
        [SerializeField] private GameObject _weaponCardsPanel;
        [SerializeField] private Button _exitButton;
        [SerializeField] private UIBossHPBar _bossHPBar;

        private UiExperienceIndicator _experienceIndicator;
        private UiCharacterLevelIndicator _levelIndicator;
        private UiTimerView _timerView;
        private UiCoinsView _coinsView;
        private UiKillCounter _killCounter;
        private UIItemsView _itemsView;
        private UIStatisticView _statisticRunView;

        public event Action OnQuitButtonClick;
        public event Action OnContinueButtonClick;
        public event Action OnRespawnButtonClick;
        public event Action OnRestartButtonClick;
        public event Action OnMobilePauseClick;


        private void Awake()
        {
            _mobilePauseButton.onClick.AddListener(MobilePauseClick);
            _quinButtonInPause.onClick.AddListener(StatisticButtonClick);
            _quinButtonGameOver.onClick.AddListener(StatisticButtonClick);
            _exitButton.onClick.AddListener(ExitButtonClick);
            _continueButton.onClick.AddListener(ContinueButtonClick);
            _respawnButton.onClick.AddListener(RespawnButtonClick);
            _restartButton.onClick.AddListener(RestartButtonClick);

            _experienceIndicator = new UiExperienceIndicator(_expiriensBarFiller);
            _levelIndicator = new UiCharacterLevelIndicator(_characterLevel);
            _timerView = new UiTimerView(_timeIndicator);
            _coinsView = new UiCoinsView(_coinIndicator);
            _killCounter = new UiKillCounter(_killCounterText);
            _itemsView = new UIItemsView(_weaponCells, _passiveItemCells);
            _statisticRunView = new UIStatisticView(_globalStatText, _weaponCardsPanel, _prefabWeaponCard);

            HidePauseMenu();
            HideGameOverPanel();
            HideStatisticPanel();
            HideMobileUI();
        }

        private void OnDisable()
        {
            _mobilePauseButton.onClick.RemoveListener(MobilePauseClick);
            _quinButtonInPause.onClick.RemoveListener(ExitButtonClick);
            _quinButtonGameOver.onClick.RemoveListener(ExitButtonClick);
            _continueButton.onClick.RemoveListener(ContinueButtonClick);
            _respawnButton.onClick.RemoveListener(RespawnButtonClick);
            _restartButton.onClick.RemoveListener(RestartButtonClick);
        }

        public void HideMobileUI()
        {
            if (!Application.isMobilePlatform)
            {
                _mobilePauseButton.gameObject.SetActive(false);
            }
        }

        public void ShowPauseMenu()
        {
            _pauseMenuPanel.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _pauseMenuPanel.SetActive(false);
        }

        public void ShowGameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void HideGameOverPanel()
        {
            _gameOverPanel.SetActive(false);
        }

        public void ShowStatisticPanel()
        {
            _statisticRunView.OnShowStatistic?.Invoke();
            _statisticRunPanel.SetActive(true);
        }

        public void HideStatisticPanel()
        {
            _statisticRunPanel.SetActive(false);
        }

        public void ShowRespawnButton()
        {
            _respawnButton.gameObject.SetActive(true);
        }
        
        public void HideRespawnButton()
        {
            _respawnButton.gameObject.SetActive(false);
        }

        public void ShowLoadingScreen()
        {
            Time.timeScale = 0f;
            _loadingScreen.SetActive(true);
        }
        
        public void HideLoadingScreen()
        {
            Time.timeScale = 1f;
            _loadingScreen.SetActive(false);
        }

        public void ShowBossHPBar(IRespawnable enemyView)
        {
            _bossHPBar.SetBossEnemyView(enemyView);
            _bossHPBar.Show();
            enemyView.OnDie += HideBossHPBar;
        }

        public void HideBossHPBar(IRespawnable enemyView)
        {
            _bossHPBar.Hide();
            enemyView.OnDie -= HideBossHPBar;
        }

        public IExperienceView GetIExperienceIndicator()
        {
            return _experienceIndicator;
        }

        public ICharacterLevelView GetCharacterLevelIndicator()
        {
            return _levelIndicator;
        }

        public ITimerView GetTimerView()
        {
            return _timerView;
        }

        public ICoinsView GetCoinsView()
        {
            return _coinsView;
        }

        public IKillCouterView GetKillCouter()
        {
            return _killCounter;
        }

        public IItemsView GetItemsView()
        {
            return _itemsView;
        }

        public UIStatisticView GetStatistiсView()
        {
            return _statisticRunView;
        }

        private void MobilePauseClick()
        {
            OnMobilePauseClick?.Invoke();
        }

        private void ExitButtonClick()
        {
            OnQuitButtonClick?.Invoke();
            HideGameOverPanel();
            HidePauseMenu();
            HideStatisticPanel();
        }

        private void StatisticButtonClick()
        {
            HideGameOverPanel();
            HidePauseMenu();
            ShowStatisticPanel();
        }

        private void ContinueButtonClick()
        {
            OnContinueButtonClick?.Invoke();
        }
        
        private void RespawnButtonClick()
        {
            OnRespawnButtonClick?.Invoke();
            HideGameOverPanel();
            Time.timeScale = 1f;
        }

        private void RestartButtonClick()
        {
            OnRestartButtonClick?.Invoke();
        }

        public void ShowChestUI(IUpgradeInfo[] appliedUpgrades)
        {
            Time.timeScale = 0.0f;
            int index = 0;
            foreach (IUpgradeInfo upgrade in appliedUpgrades)
            {
                _chestPanelView.SetAndActivateImage(upgrade.Icon, index);
                index++;
                if (index > 4)
                {
                    _chestPanelView.ShowEpic();
                    _chestPanelView.Show();
                    return;
                }
            }

            if (index == 1)
            {
                _chestPanelView.ShowCommon();
            }
            else if (index <= 4)
            {
                _chestPanelView.ShowRare();
            }
            else
            {
                _chestPanelView.ShowEpic();
            }
            _chestPanelView.Show();
        }

        public void SetGameOverText(string gameOverText)
        {
            _gameOverText.text = gameOverText;
        }

        public override void Hide()
        {
            base.Hide();
            HidePauseMenu();
            HideGameOverPanel();
        }
    }
}