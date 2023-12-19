using Core.Interface;
using TMPro;

namespace UI
{
    internal sealed class UiCoinsView : ICoinsView
    {
        private readonly TMP_Text _text;

        public UiCoinsView(TMP_Text coinsText)
        {
            _text = coinsText;
        }


        public void SetCoins(int coins)
        {
            _text.text = coins.ToString();
        }
    }
}