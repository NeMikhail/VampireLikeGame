using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Configs;
using Core;
using Core.Interface.IPresenters;
using Core.ResourceLoader;

namespace UI
{
    internal sealed class UIModifiersSelect : ICleanUp
    {
        private const int NONE_SELECTED = -1;
        private const int LEVEL_START = 1;

        private readonly Transform _content;

        private UpgradeConfigPack _upgradeConfigPack;
        private List<UIMenuUpgradeButton> _menuUpgradeButtons;
        private UpgradeConfig[] _upgradeConfigs;
        private UIMenuUpgradeButton _prefabButton;

        private int _selectedButton = NONE_SELECTED;
        private bool _isUpgradeButtonListCreated;
        
        public List<UIMenuUpgradeButton> UpgradeButtons => _menuUpgradeButtons;

        public event Action<UpgradeConfig, UIMenuUpgradeButton> OnUpgradeButtonSelected;

        public UIModifiersSelect(Transform contetnt)
        {
            _content = contetnt;
        }


        public void CreateUpgradeButtonList()
        {
            if (!_isUpgradeButtonListCreated)
            {
                _isUpgradeButtonListCreated = true;
                _upgradeConfigPack = ResourceLoadManager.GetConfig<UpgradeConfigPack>();

                if (_upgradeConfigPack)
                {
                    _prefabButton = ResourceLoadManager.
                        GetPrefabComponentOrGameObject<UIMenuUpgradeButton>(ConstantsProvider.UPGRADE_BUTTON_PREFAB_NAME);

                    _upgradeConfigs = _upgradeConfigPack.UpgradeConfigs;
                    _menuUpgradeButtons = new List<UIMenuUpgradeButton>();

                    for (int i = 0; i < _upgradeConfigs.Length; i++)
                    {
                        UpgradeConfig upgradeConfig = _upgradeConfigs[i];
                        UIMenuUpgradeButton upgradeButton = Object.Instantiate(_prefabButton, _content);

                        _menuUpgradeButtons.Add(upgradeButton);

                        upgradeButton.IsSelected = false;
                        upgradeButton.Index = i;
                        upgradeButton.UpgradeName.text = upgradeConfig.UpgradeName;
                        upgradeButton.CurrentLevel = upgradeConfig.CurrentLevel.ToString();
                        upgradeButton.UpgradeImage.sprite = upgradeConfig.UpgradeImage;

                        upgradeButton.OnUpgradeButtonClick += OnButtonClick;
                    }
                }
            }
        }

        public void ResetUpgradeButtonList()
        {
            foreach(UIMenuUpgradeButton upgradeButton in _menuUpgradeButtons)
            {
                upgradeButton.CurrentLevel = LEVEL_START.ToString();
            }
        }

        private void OnButtonClick(int index)
        {
            if (index == _selectedButton)
            {
                _menuUpgradeButtons[_selectedButton].IsSelected = false;
                _selectedButton = NONE_SELECTED;
            }
            else
            {
                if (_selectedButton >= 0)
                {
                    _menuUpgradeButtons[_selectedButton].IsSelected = false;
                }

                _selectedButton = index;
                _menuUpgradeButtons[_selectedButton].IsSelected = true;

                UpgradeConfig upgradeConfig = _upgradeConfigPack.UpgradeConfigs[_selectedButton];

                OnUpgradeButtonSelected?.Invoke(upgradeConfig, _menuUpgradeButtons[_selectedButton]);
            }
        }

        public void Cleanup()
        {
            for(int i = 0; i < _menuUpgradeButtons.Count; i++)
            {
                _menuUpgradeButtons[i].OnUpgradeButtonClick -= OnButtonClick;
            }
        }
    }
}