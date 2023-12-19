using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class UiStageDescriptionPanel : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public Image PanelBackground { get; private set; }
        [field: SerializeField] public Image LocationImage { get; private set; }
        [field: SerializeField] public Text LocationName { get; private set; }
        [field: SerializeField] public Text LocationDescription { get; private set; }

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _notSelectedColor;

        public event Action<int> PointerClick;

        private int _index;
        private bool _isSelected;

        public int Index { get => _index; set => _index = value; }
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


        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick?.Invoke(_index);
        }
    }
}