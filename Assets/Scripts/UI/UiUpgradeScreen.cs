using System;
using UnityEngine;
using Core.Interface;
using Structs.UpgradeSystem;

namespace UI
{
    internal sealed class UiUpgradeScreen : UiBaseScreen, IUpgradeScreenView
    {
        private const int FIRST_PANEL_NUMBER = 1;

        [SerializeField] private UiUpgradePanel[] _upgradePanels;

        public event Action<int> OnUpgradeSelect;


        private void Awake()
        {
            InitializePanels();
        }

        public void SetUpgrades(IUpgradeInfo[] upgrades)
        {
            if (_upgradePanels != null)
            {
                int quantity = upgrades.Length;
                for (int i = 0; i < _upgradePanels.Length; i++)
                {
                    UiUpgradePanel panel = _upgradePanels[i];
                    if (i < quantity)
                    {
                        panel.SetUpgradeInfo(upgrades[i]);
                        panel.gameObject.SetActive(true);
                    }
                    else
                    {
                        panel.gameObject.SetActive(false);
                    }
                }
            }
        }

        private void InitializePanels()
        {
            if (_upgradePanels != null)
            {
                int number = FIRST_PANEL_NUMBER;
                for (int i = 0; i < _upgradePanels.Length; i++)
                {
                    _upgradePanels[i].Number = number;
                    number++;
                    _upgradePanels[i].OnClick += OnPanelClick;
                }
            }
        }

        private void OnPanelClick(int number)
        {
            OnUpgradeSelect?.Invoke(number - FIRST_PANEL_NUMBER);
        }
    }
}