using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio.Service
{
    public sealed class AudioService : MonoBehaviour
    {
        private const float MAX_MIXER_VOLUME = 0f;
        private const float MIN_MIXER_VOLUME = -80f;
        
        [SerializeField] private AudioClipHolder _audioClipHolder;
        [SerializeField] private AudioSource _effectAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _fadeInMusicAudioSource;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _masterGroup = "Master";
        [SerializeField] private string _musicGroup = "Music";
        [SerializeField] private string _effectsGroup = "Effects";

        private static AudioService s_instance;

        public static AudioService Instance => s_instance;


        private void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_musicAudioSource != null)
            {
                _musicAudioSource.Stop();
            }

            if (_fadeInMusicAudioSource != null)
            {
                _fadeInMusicAudioSource.Stop();
            }
        }

        public async UniTask FadeOutPlayMusic(float fadeTime)
        {
            float startVolume = _musicAudioSource.volume;

            while (_musicAudioSource.volume > 0)
            {
                if (this == null) break;
                _musicAudioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                await UniTask.Yield();
            }

            _musicAudioSource.Stop();
            _musicAudioSource.volume = startVolume;
        }

        public async UniTask SwapPlayMusic(AudioClipNames audioName, float fadeTime, float volumeScale)
        {
            float startVolume = 0f;
            float startVolume2 = 0f;
            float fadeSpeed = Time.deltaTime / fadeTime;
            bool isMusicPlaying = _musicAudioSource.isPlaying;

            if (isMusicPlaying)
            {
                startVolume = _musicAudioSource.volume;

                while (_musicAudioSource.volume > 0.5f)
                {
                    if (this == null) break;
                    _musicAudioSource.volume -= startVolume * fadeSpeed;
                    await UniTask.Yield();
                }

                _musicAudioSource.volume = startVolume;
            }
            else
            {
                _fadeInMusicAudioSource.Stop();
            }

            _fadeInMusicAudioSource.volume = startVolume2;
            _fadeInMusicAudioSource.clip = GetAudioClip(audioName);
            _fadeInMusicAudioSource.loop = true;
            _fadeInMusicAudioSource.Play();

            while (startVolume2 < volumeScale)
            {
                if (this == null) break;
                startVolume2 += fadeSpeed;
                if (isMusicPlaying)
                {
                    _musicAudioSource.volume -= startVolume * fadeSpeed;
                }

                _fadeInMusicAudioSource.volume = startVolume2;
                await UniTask.Yield();
            }

            if (_musicAudioSource != null || _fadeInMusicAudioSource != null)
            {
                _musicAudioSource.Stop();
                _fadeInMusicAudioSource.volume = volumeScale;

                if (!_musicAudioSource.isPlaying)
                {
                    _musicAudioSource.clip = _fadeInMusicAudioSource.clip;
                    _musicAudioSource.volume = _fadeInMusicAudioSource.volume;
                    _musicAudioSource.time = _fadeInMusicAudioSource.time;
                    _fadeInMusicAudioSource.Stop();
                    _fadeInMusicAudioSource.clip = null;
                    _musicAudioSource.Play();
                }
            }
        }

        public async void PlayAudioOneShot(AudioClipNames audioName, bool isSidechain = false)
        {
            AudioClip audioClip = GetAudioClip(audioName); 

            if (audioClip == null)
            {
                Debug.Log($"Can't find audio clip from {audioName.ToString()}");
                return;
            }

            if (isSidechain)
            {
                _audioMixer.GetFloat(_musicGroup, out float currentMusicVolume);
                _audioMixer.SetFloat(_musicGroup, -80f);
                _effectAudioSource.PlayOneShot(audioClip);
                await UniTask.RunOnThreadPool(FadeIn);
                _audioMixer.SetFloat(_musicGroup, currentMusicVolume);
            }
            else
            {
                _effectAudioSource.PlayOneShot(audioClip);
            }
        }

        private async UniTask FadeIn()
        {
            float fadeTime = 10f;
            float volumeScale = 1f;
            await UniTask.SwitchToMainThread();
            float musicVolume = _musicAudioSource.volume;
            while (musicVolume < volumeScale)
            {
                musicVolume += Time.unscaledDeltaTime * fadeTime;
                _musicAudioSource.volume = musicVolume;
                await UniTask.Yield();
            }
            _musicAudioSource.volume = volumeScale;
        }

        private async UniTask FadeOut()
        {
            float fadeTime = 70f;
            float volumeScale = -10f;
            await UniTask.SwitchToMainThread();
            while (_audioMixer.GetFloat(_musicGroup, out float musicVolume) && musicVolume > volumeScale)
            {
                musicVolume -= Time.unscaledDeltaTime * fadeTime;
                _audioMixer.SetFloat(_musicGroup, musicVolume);
                await UniTask.Yield();
            }
        }

        public void PlayAudio(AudioClipNames audioName)
        {
            AudioClip audioClip = GetAudioClip(audioName);

            if (audioClip == null)
            {
                Debug.Log($"Can't find audio clip from {audioName.ToString()}");
                return;
            }

            _musicAudioSource.clip = audioClip;
            _musicAudioSource.loop = true;
            _musicAudioSource.Play();
        }

        public void ChangeMasterVolume(float volume)
        {
            ChangeVolume(_masterGroup, volume);
        }
        
        public void ChangeMusicVolume(float volume)
        {
            ChangeVolume(_musicGroup, volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            
            ChangeVolume(_effectsGroup, volume);
        }

        public float GetCurrentMasterVolume()
        {
            _audioMixer.GetFloat(_masterGroup, out float volume);
            return volume;
        }
        
        public float GetCurrentMusicVolume()
        {
            _audioMixer.GetFloat(_musicGroup, out float volume);
            return volume;
        }
        
        public float GetCurrentEffectVolume()
        {
            _audioMixer.GetFloat(_effectsGroup, out float volume);
            return volume;
        }

        public void ToggleMusic(bool isOn) =>
            _musicAudioSource.mute = !isOn;

        public void ToggleEffects(bool isOn) =>
            _effectAudioSource.mute = !isOn;

        private void ChangeVolume(string mixerGroup, float volume)
        {
            if ((volume >= MIN_MIXER_VOLUME) && (volume <= MAX_MIXER_VOLUME))
            {
                _audioMixer.SetFloat(mixerGroup, volume);
            }
            else if (volume <= MIN_MIXER_VOLUME)
            {
                _audioMixer.SetFloat(mixerGroup, MIN_MIXER_VOLUME);
            }
            else if (volume >= MAX_MIXER_VOLUME)
            {
                _audioMixer.SetFloat(mixerGroup, MAX_MIXER_VOLUME);
            }
        }

        private AudioClip GetAudioClip(AudioClipNames audioName)
        {
            foreach (AudioData clip in _audioClipHolder.AudioData)
            {
                if (clip.AudioClipName == audioName)
                    return clip.AudioClip;
            }

            return null;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Silence(!hasFocus);
        }

        private void Silence(bool silence)
        {
            AudioListener.pause = silence;
            AudioListener.volume = silence ? 0 : 1;
        }
    }
}
