using Audio.Service;
using Core.Interface;
using Core.Interface.IPresenters;
using Core.SaveClass;
using Core.SaveClass.SaveModules;
using Enums;
using UI;

namespace MVP.Presenters.UIPresenters
{
    internal sealed class UISettingsPresenter : IInitialisation, ICleanUp
    {
        private const float LOCAL_MIN_MIXER_VOLUME = -20f;
        private const float MIN_MIXER_VOLUME = -80f;
        
        private AudioService _audioService;
        private UiMainMenuScreen _uiMainMenuScreen;

        public UISettingsPresenter(IViewProvider viewProvider)
        {
            _uiMainMenuScreen = viewProvider.GetMenuScreen();
        }
        

        public void Initialisation()
        {
            _audioService = AudioService.Instance;
            _uiMainMenuScreen.OnMasterSliderChanged += MasterVolumeChanged;
            _uiMainMenuScreen.OnMusicSliderChanged += MusicVolumeChanged;
            _uiMainMenuScreen.OnEffectsSliderChanged += EffectsVolumeChanged;
            Saver.LoadSettings(new PlayerPrefsParserModule());
            SetSlidersToCurrentVolume();
        }
        
        public void Cleanup()
        {
            _uiMainMenuScreen.OnMasterSliderChanged -= MasterVolumeChanged;
            _uiMainMenuScreen.OnMusicSliderChanged -= MusicVolumeChanged;
            _uiMainMenuScreen.OnEffectsSliderChanged -= EffectsVolumeChanged;
        }
        
        private void SetSlidersToCurrentVolume()
        {
            float currentVolume;
            float currentSliderValue;
            
            currentVolume = _audioService.GetCurrentMasterVolume();
            currentSliderValue = GetNewSliderValue(currentVolume);
            _uiMainMenuScreen.ChangeVolumeSliderValue(currentSliderValue, AudioMixers.Master);
            
            currentVolume = _audioService.GetCurrentMusicVolume();
            currentSliderValue = GetNewSliderValue(currentVolume);
            _uiMainMenuScreen.ChangeVolumeSliderValue(currentSliderValue, AudioMixers.Music);
            
            currentVolume = _audioService.GetCurrentEffectVolume();
            currentSliderValue = GetNewSliderValue(currentVolume);
            _uiMainMenuScreen.ChangeVolumeSliderValue(currentSliderValue, AudioMixers.Effects);
        }

        private float GetNewSliderValue(float currentVolume)
        {
            if (currentVolume >= LOCAL_MIN_MIXER_VOLUME)
            {
                return (currentVolume - LOCAL_MIN_MIXER_VOLUME) / -LOCAL_MIN_MIXER_VOLUME;
            }
            else
            {
                return 0f;
            }
        }
        
        private void MasterVolumeChanged(float volume)
        {
            volume = LOCAL_MIN_MIXER_VOLUME + volume * -LOCAL_MIN_MIXER_VOLUME;
            if (volume == LOCAL_MIN_MIXER_VOLUME)
            {
                _audioService.ChangeMasterVolume(MIN_MIXER_VOLUME);
            }
            else
            {
                _audioService.ChangeMasterVolume(volume);
            }
            Saver.SaveSettings(new PlayerPrefsParserModule());
        }
        
        private void MusicVolumeChanged(float volume)
        {
            volume = LOCAL_MIN_MIXER_VOLUME + volume * -LOCAL_MIN_MIXER_VOLUME;
            if (volume == LOCAL_MIN_MIXER_VOLUME)
            {
                _audioService.ChangeMusicVolume(MIN_MIXER_VOLUME);
            }
            else
            {
                _audioService.ChangeMusicVolume(volume);
            }
            Saver.SaveSettings(new PlayerPrefsParserModule());
        }
        
        private void EffectsVolumeChanged(float volume)
        {
            volume = LOCAL_MIN_MIXER_VOLUME + volume * -LOCAL_MIN_MIXER_VOLUME;
            if (volume == LOCAL_MIN_MIXER_VOLUME)
            {
                _audioService.ChangeEffectsVolume(MIN_MIXER_VOLUME);
            }
            else
            {
                _audioService.ChangeEffectsVolume(volume);
            }
            Saver.SaveSettings(new PlayerPrefsParserModule());
        }
    }
}