using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Configs;
using Core;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Enums;
using Infrastructure.Extentions;

namespace UI
{
    internal sealed class UICharacterSelect : ICleanUp
    {
        private const int NONE_SELECTED = -1;
        private readonly Transform _content;

        private PlayerScriptableConfig _playersConfig;
        private List<UICharacterViewPanel> _panels;
        private SerializableDictionary<CharacterName, CharacterConfig> _characterConfigs;

        private int _selectedPanel = NONE_SELECTED;
        private bool _isCharacterListCreated;

        public PlayerScriptableConfig PlayersConfig => _playersConfig;

        public event Action<CharacterName, CharacterConfig> OnCharacterPanelSelected;

        public UICharacterSelect(Transform content)
        {
            _content = content;
        }


        public void CreateCharacterList()
        {
            if (!_isCharacterListCreated)
            {
                _isCharacterListCreated = true;
                _playersConfig = ResourceLoadManager.GetConfig<PlayerScriptableConfig>();

                if (_playersConfig)
                {
                    UICharacterViewPanel prefab = ResourceLoadManager.
                        GetPrefabComponentOrGameObject<UICharacterViewPanel>(ConstantsProvider.CHARACTER_VIEW_PREFAB_NAME);
                    _characterConfigs = _playersConfig.Characters;

                    _panels = new List<UICharacterViewPanel>();

                    for (int i = 0; i < _characterConfigs.Length; i++)
                    {
                        CharacterConfig characterConfig = _characterConfigs.GetValueByIndex(i);
                        if(!characterConfig.IsCharachterOpen)
                        {
                            continue;
                        }
                        UICharacterViewPanel panel = Object.Instantiate(prefab, _content);

                        _panels.Add(panel);
                        
                        panel.IsSelected = false;
                        panel.Index = i;
                        panel.PlayerName.text = characterConfig.CharacterName;
                        panel.PlayerImage.sprite = characterConfig.CharacterImage;
                        panel.IsUnlocked = characterConfig.IsUnlocked;
                        characterConfig.OnUnlockedStatusChanged += panel.UnlockCharacter;
                        panel.PointerClick += OnPanelClick;
                    }
                }
            }
        }

        public CharacterName GetSelectedCharacter()
        {
            if (_selectedPanel != NONE_SELECTED)
            {
                return _characterConfigs.GetKeyByIndex(_selectedPanel);
            }

            return CharacterName.None;
        }

        private void OnPanelClick(int index)
        {
            if (index == _selectedPanel)
            {
                _panels[_selectedPanel].IsSelected = false;
                _selectedPanel = NONE_SELECTED;
            }
            else
            {
                if (_selectedPanel >= 0)
                {
                    _panels[_selectedPanel].IsSelected = false;
                }

                _selectedPanel = index;
                _panels[_selectedPanel].IsSelected = true;
                CharacterName name = GetSelectedCharacter();
                CharacterConfig character = _playersConfig.Characters.GetValue(name);
                OnCharacterPanelSelected?.Invoke(name, character);
            }
        }

        public void Cleanup()
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                _panels[i].PointerClick -= OnPanelClick;
                CharacterConfig characterConfig = _characterConfigs.GetValueByIndex(i);
                characterConfig.OnUnlockedStatusChanged -= _panels[i].UnlockCharacter; 
            }
        }
    }
}