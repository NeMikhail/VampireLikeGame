using UnityEngine.UI;
using Core.Interface;

namespace UI
{
    internal sealed class UiKillCounter : IKillCouterView
    {
        private readonly Text _text;

        public UiKillCounter(Text coinsText)
        {
            _text = coinsText;
        }

        public void SetValue(int value)
        {
            _text.text = value.ToString();
        }
    }
}
