using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Configs;
using Core.Interface.AchievementSystem;
using Core.ResourceLoader;

namespace UI
{
    internal sealed class UiAchievementScreen : UiBaseScreen
    {
        [SerializeField] private GameObject _achievementPanelPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TMP_Text _caption;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _bottomPanel;
        private List<UiAchievementPanel> _achievementPanels;
        private bool _isActiveBottomPanel = false;
        private int _achievementsCount;

        private void OnEnable()
        {
            AchievementListConfig achievementListConfig = ResourceLoadManager.GetConfig<AchievementListConfig>("AchievementList");
            _achievementPanels = new List<UiAchievementPanel>();
            SetAchievements(achievementListConfig.Achievements);
        }

        public void SetAchievements(List<AchievementListConfig.Achievement> achievements)
        {
            _achievementsCount = achievements.Count;

            for (int i = 0; i < _achievementsCount; i++)
            {
                UiAchievementPanel panel = Instantiate(_achievementPanelPrefab, _content).GetComponent<UiAchievementPanel>();
                panel.SetAchievementInfo(achievements[i]);
                panel.Show();
                panel.OnClick += OnPanelClick;
                _achievementPanels.Add(panel);
            }
        }

        private void OnPanelClick(IAchievement achievement)
        {
            if (_isActiveBottomPanel == false)
            {
                _isActiveBottomPanel = true;
                _bottomPanel.SetActive(true);
            }

            if (achievement.Unlocked)
                _caption.text = $"Открыто: {achievement.Name}";
            else
                _caption.text = $"Открывает: {achievement.Name}";

            _description.text = achievement.Description;
            _icon.sprite = achievement.Image;
        }

        private void OnDisable()
        {
            foreach (UiAchievementPanel panel in _achievementPanels)
            {
                panel.OnClick -= OnPanelClick;
                Destroy(panel.gameObject);
            }
            _achievementPanels.Clear();

            _isActiveBottomPanel = false;
            _bottomPanel.SetActive(false);
        }
    }
}
