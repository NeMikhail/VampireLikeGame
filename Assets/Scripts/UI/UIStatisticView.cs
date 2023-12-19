using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using TMPro;
using MVP.Models.StatisticsSystemModels;

namespace UI
{
    public class UIStatisticView
    {
        private TMP_Text _globalStatText;
        private GameObject _weaponPanel;
        private GameObject _weaponCard;

        public Action OnShowStatistic;

        public UIStatisticView(TMP_Text global, GameObject panelCards, GameObject weaponCard)
        {
            _globalStatText = global;
            _weaponPanel = panelCards;
            _weaponCard = weaponCard;
        }


        public void SetStatistic(string global, List<WeaponCard> weaponCards, List<int> weaponCardKillsCount)
        {
            _globalStatText.text = global;
            for (int i = 0; i < weaponCards.Count; i++)
            {
                UIWeaponCard card = Object.Instantiate(_weaponCard).GetComponent<UIWeaponCard>();
                card.gameObject.transform.SetParent(_weaponPanel.transform);
                card.WeaponImage.sprite = weaponCards[i].Image;
                card.WeaponStat.text = weaponCards[i].Name + ": " + weaponCardKillsCount[i];
            }
        }
    }
}