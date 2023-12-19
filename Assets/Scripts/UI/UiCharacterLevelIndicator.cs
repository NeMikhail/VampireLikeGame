using System;
using UnityEngine.UI;

using Core.Interface;


namespace UI
{
    internal sealed class UiCharacterLevelIndicator : ICharacterLevelView
    {
        private const string PREFEX_LV = "Lv ";

        private readonly Text _text;

        public UiCharacterLevelIndicator(Text levelText)
        {
            _text = levelText;
        }

        public void SetLevel(int level)
        {
            _text.text = PREFEX_LV + level.ToString();
        }

    }
}
