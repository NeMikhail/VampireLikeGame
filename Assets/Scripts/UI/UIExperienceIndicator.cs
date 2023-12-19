using System;
using UnityEngine;

using Core.Interface;


namespace UI
{
    internal sealed class UiExperienceIndicator : IExperienceView
    {

        private const int MIN_MAX_EXPERIENCE = 1;

        private readonly RectTransform _bar;

        private int _maxValue;


        public UiExperienceIndicator(RectTransform bar)
        {
            _bar = bar;
        }

        public void SetValues(int maxValue, int currentValue)
        {
            _maxValue = maxValue;
            if (_maxValue < MIN_MAX_EXPERIENCE)
            {
                _maxValue = MIN_MAX_EXPERIENCE;
            }
            SetCurrentValue(currentValue);
        }

        public void SetCurrentValue(int value)
        {
            value = Mathf.Clamp(value, 0, _maxValue);
            Vector2 offsetMax = _bar.offsetMax;
            float width = _bar.rect.width - offsetMax.x;
            offsetMax.x = -(float)(_maxValue - value) / _maxValue * width;
            _bar.offsetMax = offsetMax;
        }

    }
}
