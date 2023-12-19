using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Structs.UpgradeSystem;

namespace UI
{
    internal sealed class UiUpgradePanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _caption;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;

        public event Action<int> OnClick;

        private int _number;

        public int Number 
        { 
            get => _number; 
            set => _number = value; 
        }


        public void SetUpgradeInfo(IUpgradeInfo info)
        {
            _caption.text = info.Caption;
            _description.text = info.Description;
            _icon.sprite = info.Icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_number);
        }
    }
}