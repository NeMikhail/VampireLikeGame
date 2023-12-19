using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public sealed class AudioData
    {
        public AudioClipNames AudioClipName;
        public AudioClip AudioClip;
    }
}