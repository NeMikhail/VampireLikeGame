using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class UIChestPanelView : UiBaseScreen
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private GameObject _commonChestImage;
        [SerializeField] private GameObject _rareChestImage;
        [SerializeField] private GameObject _epicChestImage;
        [SerializeField] private List<Image> _itemsImages;


        private void Start()
        {
            _continueButton.onClick.AddListener(Continue);
        }

        public void SetAndActivateImage(Sprite icon, int index)
        {
            if (icon != null)
            {
                _itemsImages[index].gameObject.SetActive(true);
                _itemsImages[index].sprite = icon;
            }
        }

        public void ShowCommon()
        {
            _commonChestImage.SetActive(true);
        }
        
        public void ShowRare()
        {
            _rareChestImage.SetActive(true);
        }
        
        public void ShowEpic()
        {
            _epicChestImage.SetActive(true);
        }

        private void Continue()
        {
            Time.timeScale = 1.0f;
            _commonChestImage.SetActive(false);
            _rareChestImage.SetActive(false);
            _epicChestImage.SetActive(false);

            foreach (Image itemImage in _itemsImages)
            {
                itemImage.gameObject.SetActive(false);
            }
            Hide();
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
        }
    }
}