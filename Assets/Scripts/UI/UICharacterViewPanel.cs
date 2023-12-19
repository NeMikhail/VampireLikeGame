using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UICharacterViewPanel : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public Image PanelBackground { get; private set; }
        [field: SerializeField] public Image PlayerImage { get; private set; }
        [field: SerializeField] public Text PlayerName { get; private set; }

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _notSelectedColor;
        [SerializeField] private GameObject _lockedMask;
        
        public event Action<int> PointerClick;

        private int _index;
        private bool _isSelected;
        
        public int Index
        {
            get => _index;
            set => _index = value;
        }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    PanelBackground.color = _selectedColor;
                }
                else
                {
                    PanelBackground.color = _notSelectedColor;
                }
            }
        }
        public bool IsUnlocked
        {
            set { _lockedMask.SetActive(!value); }
        }
        

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick?.Invoke(_index);
        }

        public void UnlockCharacter(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
        }
    }
}