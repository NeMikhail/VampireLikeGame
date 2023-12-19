using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    internal sealed class UIMenuUpgradeButton : UiBaseScreen
    {
        [field: SerializeField] public TMP_Text UpgradeName { get; private set; }
        [field: SerializeField] public Image UpgradeImage { get; private set; }

        [SerializeField] private TMP_Text _currentLevel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _background;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _notSelectedColor;

        private int _index;
        private bool _isSelected;

        public string CurrentLevel
        {
            get => _currentLevel.text;
            set => _currentLevel.text = value;
        }
        public Image Background => _background;
        public int Index
        {
            get => _index;
            set => _index = value;
        }
        public bool IsSelected
        {
            get
            { 
                return _isSelected; 
            }
            set
            {
                _isSelected = value;
                if (_isSelected)
                    Background.color = _selectedColor;
                else
                    Background.color = _notSelectedColor;
            }
        }

        public event Action<int> OnUpgradeButtonClick;


        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick()
        {
            OnUpgradeButtonClick?.Invoke(_index);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveAllListeners();
        }
    }
}