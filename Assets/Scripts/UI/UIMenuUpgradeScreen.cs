using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    internal sealed class UIMenuUpgradeScreen : UiBaseScreen
    {
        [SerializeField] private TMP_Text _upgradeCost;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _resetButton;

        public string UpgradeCost
        {
            set => _upgradeCost.text = value;
        }

        public event Action OnBuyButtonClick;
        public event Action OnResetButtonClick;


        private void OnEnable()
        {
            _buyButton.onClick.AddListener(BuyButtonClicked);
            _resetButton.onClick.AddListener(ResetButtonClicked);
        }

        private void BuyButtonClicked()
        {
            OnBuyButtonClick?.Invoke();
        }

        private void ResetButtonClicked()
        {
            OnResetButtonClick?.Invoke();
        }

        private void OnDisable()
        {
            _buyButton?.onClick.RemoveListener(BuyButtonClicked);
            _resetButton?.onClick.RemoveListener(ResetButtonClicked);
        }
    }
}