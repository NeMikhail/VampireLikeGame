using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using Audio;
using Audio.Service;
using Enums;

namespace UI
{
    public sealed class UiMainMenuScreen : UiBaseScreen
    {
        [SerializeField] private TMP_Text _buildVersionText;
        [SerializeField] private TMP_Text _coinIndicator;

        [Header("Main buttons group"), SerializeField]
        private GameObject _buttonsRoot;
        [SerializeField] private Button _clearProgressButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _analyticsStartButton;

        [SerializeField] private Button _startMainButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _achivmentsButton; 
        
        [Header("Settings panel"), SerializeField] 
        private GameObject _settingsRoot;
        [SerializeField] private Button _closeSettingssButton;
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        
        [Header("Upgrade panel"), SerializeField]
        private GameObject _upgradeRoot;

        [SerializeField] private UIMenuUpgradeScreen _menuUpgradeScreen;
        [SerializeField] private Button _closeUpgradesButton;
        [field: SerializeField] public Transform ModifiersContent { get; private set; }

        [Header("Achivments panel"), SerializeField]
        private GameObject _achivmentsRoot;

        [SerializeField] private Button _closeAchivmentsButton;

        [Header("Character select panel"), SerializeField]
        private GameObject _characterSelectRoot;

        [SerializeField] private UICharacterBuyPanelView _characterBuyPanelView;
        [SerializeField] private Button _closeCharSelectButton;
        [SerializeField] private Button _startGameCharSelect;
        [field: SerializeField] public Transform CharacterSelectContent { get; private set; }

        [Header("Stage select panel"), SerializeField]
        private GameObject _stageSelectRoot;

        [SerializeField] private Button _closeStageSelectButton;
        [SerializeField] private Button _startGameStageSelect;
        [field: SerializeField] public Transform StageSelectContent { get; private set; }

        [Header("Credits panel"), SerializeField]
        private GameObject _creditsRoot;

        [SerializeField] private Button _closeCreditsButton;

        [Header("Quit confirm panel"), SerializeField]
        private GameObject _clearConfirmRoot;

        [SerializeField] private Button _clearConfirmButton;
        [SerializeField] private Button _closeConfirmButton;

        private UiCoinsView _coinsView;

        public event Action OnConfirmClearButtonClick;
        public event Action OnStartGameButtonClick;
        public event Action OnAnalyticsButtonClick;

        public event Action<float> OnMasterSliderChanged;
        public event Action<float> OnMusicSliderChanged;
        public event Action<float> OnEffectsSliderChanged;

        internal UiCoinsView CoinsView => _coinsView;
        internal UICharacterBuyPanelView CharacterBuyPanelView => _characterBuyPanelView;
        internal UIMenuUpgradeScreen UIMenuUpgradeScreen => _menuUpgradeScreen;


        private void Awake()
        {
            _upgradeRoot.SetActive(false);
            _achivmentsRoot.SetActive(false);
            _characterSelectRoot.SetActive(false);
            _stageSelectRoot.SetActive(false);
            _creditsRoot.SetActive(false);
            _clearConfirmRoot.SetActive(false);
            _coinsView = new UiCoinsView(_coinIndicator);
            _buildVersionText.text = $" Version : {Application.version}";
        }

        private void Start()
        {
            AudioService.Instance.SwapPlayMusic(AudioClipNames.Main_Music, 2f, 1).Forget();
        }

        private void OnEnable()
        {
            _clearConfirmButton.onClick.AddListener(ConfirmClearButtonClick);
            _clearProgressButton.onClick.AddListener(QuitConfirmButtonClick);
            _closeConfirmButton.onClick.AddListener(CloseQuitConfirmButtonClick);
            _settingsButton.onClick.AddListener(SettingsMenuButtonClick);
            _startMainButton.onClick.AddListener(StartMainButtonClick);
            _upgradeButton.onClick.AddListener(UpgradeButtonClick);
            _achivmentsButton.onClick.AddListener(AchivmentsButtonClick);
            _closeSettingssButton.onClick.AddListener(CloseSettingsClick);
            _creditsButton.onClick.AddListener(CreditsButtonClick);
            _closeUpgradesButton.onClick.AddListener(CloseUpgradesClick);
            _closeAchivmentsButton.onClick.AddListener(CloseAchivmentsClick);
            _closeCharSelectButton.onClick.AddListener(CloseCharacterSelectClick);
            _closeCreditsButton.onClick.AddListener(CloseCreditsClick);
            _startGameCharSelect.onClick.AddListener(StartGameFromCharSelect);
            _closeStageSelectButton.onClick.AddListener(CloseStageSelect);
            _startGameStageSelect.onClick.AddListener(StartGameFromStageSelect);
            _analyticsStartButton.onClick.AddListener(OnAnalyticsStartButtonClick);
            _masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
            _musicSlider.onValueChanged.AddListener(MusicVolumeChanged);
            _effectsSlider.onValueChanged.AddListener(EffectsVolumeChanged);
        }

        private void OnDisable()
        {
            _clearConfirmButton.onClick.RemoveListener(ConfirmClearButtonClick);
            _clearProgressButton.onClick.RemoveListener(QuitConfirmButtonClick);
            _closeConfirmButton.onClick.RemoveListener(CloseQuitConfirmButtonClick);
            _settingsButton.onClick.RemoveListener(SettingsMenuButtonClick);
            _startMainButton.onClick.RemoveListener(StartMainButtonClick);
            _upgradeButton.onClick.RemoveListener(UpgradeButtonClick);
            _achivmentsButton.onClick.RemoveListener(AchivmentsButtonClick);
            _closeSettingssButton.onClick.RemoveListener(CloseSettingsClick);
            _creditsButton.onClick.RemoveListener(CreditsButtonClick);
            _closeUpgradesButton.onClick.RemoveListener(CloseUpgradesClick);
            _closeAchivmentsButton.onClick.RemoveListener(CloseAchivmentsClick);
            _closeCharSelectButton.onClick.RemoveListener(CloseCharacterSelectClick);
            _closeCreditsButton.onClick.RemoveListener(CloseCreditsClick);
            _startGameCharSelect.onClick.RemoveListener(StartGameFromCharSelect);
            _closeStageSelectButton.onClick.RemoveListener(CloseStageSelect);
            _startGameStageSelect.onClick.RemoveListener(StartGameFromStageSelect);
            _analyticsStartButton.onClick.RemoveListener(OnAnalyticsStartButtonClick);
            _masterVolumeSlider.onValueChanged.RemoveListener(MasterVolumeChanged);
            _musicSlider.onValueChanged.RemoveListener(MusicVolumeChanged);
            _effectsSlider.onValueChanged.RemoveListener(EffectsVolumeChanged);
        }

        public void ChangeVolumeSliderValue(float value, AudioMixers audioMixer)
        {
            if (audioMixer == AudioMixers.Master)
            {
                _masterVolumeSlider.value = value;
            }
            else if (audioMixer == AudioMixers.Music)
            {
                _musicSlider.value = value;
            }
            else if (audioMixer == AudioMixers.Effects)
            {
                _effectsSlider.value = value;
            }
        }

        private void QuitConfirmButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _clearConfirmRoot.SetActive(true);
        }

        private void CloseQuitConfirmButtonClick()
        {
            _buttonsRoot.SetActive(true);
            _clearConfirmRoot.SetActive(false);
        }

        private void ConfirmClearButtonClick()
        {
            OnConfirmClearButtonClick?.Invoke();
        }

        private void SettingsMenuButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _settingsRoot.SetActive(true);
        }

        private void StartMainButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _characterSelectRoot.SetActive(true);
        }

        private void UpgradeButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _upgradeRoot.SetActive(true);
        }

        private void AchivmentsButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _achivmentsRoot.SetActive(true);
        }

        private void OnAnalyticsStartButtonClick()
        {
            OnAnalyticsButtonClick?.Invoke();
        }

        private void CloseSettingsClick()
        {
            _settingsRoot.SetActive(false);
            _buttonsRoot.SetActive(true);
        }

        private void CreditsButtonClick()
        {
            _buttonsRoot.SetActive(false);
            _creditsRoot.SetActive(true);
        }

        private void CloseUpgradesClick()
        {
            _upgradeRoot.SetActive(false);
            _buttonsRoot.SetActive(true);
        }

        private void CloseAchivmentsClick()
        {
            _achivmentsRoot.SetActive(false);
            _buttonsRoot.SetActive(true);
        }

        private void CloseCharacterSelectClick()
        {
            _characterSelectRoot.SetActive(false);
            _buttonsRoot.SetActive(true);
        }

        private void CloseCreditsClick()
        {
            _creditsRoot.SetActive(false);
            _buttonsRoot.SetActive(true);
        }

        private void StartGameFromCharSelect()
        {
            if (!_characterBuyPanelView.isActiveAndEnabled)
            {
                _characterSelectRoot.SetActive(false);
                _stageSelectRoot.SetActive(true);
            }
        }

        private void StartGameFromStageSelect()
        {
            OnStartGameButtonClick?.Invoke();
        }

        private void CloseStageSelect()
        {
            _stageSelectRoot.SetActive(false);
            _characterSelectRoot.SetActive(true);
        }

        private void MasterVolumeChanged(float volume)
        {
            OnMasterSliderChanged?.Invoke(volume);
        }
        
        private void MusicVolumeChanged(float volume)
        {
            OnMusicSliderChanged?.Invoke(volume);
        }
        
        private void EffectsVolumeChanged(float volume)
        {
            OnEffectsSliderChanged?.Invoke(volume);
        }
    }
}