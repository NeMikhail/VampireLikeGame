using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Core.Interface;
using Core.Interface.AchievementSystem;

namespace UI
{
    internal sealed class UiAchievementPanel : UiBaseScreen, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _background;

        [Header("Background color")]
        [SerializeField] private Color _openAchievementColor;
        [SerializeField] private Color _closedAchievementColor;

        private IAchievement _achievement;

        public event Action<IAchievement> OnClick;


        public void SetAchievementInfo(IAchievement achievement)
        {
            _achievement = achievement;
            _description.text = achievement.Description;
            _icon.sprite = achievement.Image;
            _toggle.isOn = achievement.Unlocked;

            if (_toggle.isOn)
                _background.color = _openAchievementColor;
            else
                _background.color = _closedAchievementColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_achievement);
        }
    }
}