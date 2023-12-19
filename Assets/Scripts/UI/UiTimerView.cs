using UnityEngine.UI;
using Core.Interface;

namespace UI
{
    internal sealed class UiTimerView : ITimerView
    {

        private const string SEPARATOR = ":";
        private const string LETTER_NULL = "0";

        private const int SECONDS_IN_MINUTE = 60;
        private const int TWO_NUMERAL_MIN_NUMBER = 10;

        private Text _timeText;


        public UiTimerView(Text text)
        {
            _timeText = text;
        }

        public void SetTime(int totalSeconds)
        {
            int minuts;
            int seconds;

            minuts = totalSeconds / SECONDS_IN_MINUTE;
            seconds = totalSeconds % SECONDS_IN_MINUTE;

            string text = minuts.ToString() + SEPARATOR;

            if (seconds < TWO_NUMERAL_MIN_NUMBER)
            {
                text += LETTER_NULL;
            }
            text += seconds.ToString();

            _timeText.text = text;
        }
    }
}
