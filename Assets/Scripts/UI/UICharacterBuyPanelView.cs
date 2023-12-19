using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Configs;
using Enums;

namespace UI
{
    public sealed class UICharacterBuyPanelView : UiBaseScreen
    {
        [SerializeField] private TMP_Text _characterCost;
        [SerializeField] private Button _buyButton;

        private CharacterConfig _character;
        private CharacterName _characterName;

        public string CharacterCost
        {
            set => _characterCost.text = value;
        }
        public CharacterName CharacterName
        {
            get => _characterName;
            set => _characterName = value;
        }
        public CharacterConfig Character
        {
            get => _character;
            set => _character = value;
        }

        public event Action OnBuyButtonClick;
        

        void Start()
        {
            _buyButton.onClick.AddListener(BuyButtonClicked);
        }
        
        private void BuyButtonClicked()
        {
            OnBuyButtonClick?.Invoke();
        }
    }
}