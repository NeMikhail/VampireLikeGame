using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;
using Audio.Service;
using Configs;
using Core;
using Core.Interface;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Core.SaveClass;
using Core.SaveClass.SaveModules;
using MVP.Presenters.InputPresenters;
using UI;

namespace MVP.Presenters.GameState
{
    internal sealed class EndGamePresenter : IInitialisation, ICleanUp
    {
        private const string YOU_WIN = "You win!!!";

        private readonly UiBattleScreen _battleScreen;
        private readonly IPlayerModel _playerModel;
        private readonly PausePresenter _pausePresenter;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private MetaprogressionConfig _metaprogressionConfig;

        public event Action OnGameEnded;

        public EndGamePresenter(IViewProvider viewProvider, IDataProvider dataProvider, PausePresenter pausePresenter)
        {
            _battleScreen = viewProvider.GetBattleScreen();
            _playerModel = dataProvider.PlayerModel;
            _pausePresenter = pausePresenter;
            _runStatisticsSystemModel = dataProvider.RunStatisticsModel;
            _metaprogressionConfig = ResourceLoadManager.GetConfig<MetaprogressionConfig>();
        }


        public void Initialisation()
        {
            _battleScreen.OnQuitButtonClick += QuitGame;
            _battleScreen.OnRestartButtonClick += RestartGame;
            _playerModel.OnPlayerDeath += PlayerDead;
        }

        public void Cleanup()
        {
            _battleScreen.OnQuitButtonClick -= QuitGame;
            _battleScreen.OnRestartButtonClick -= RestartGame;
            _playerModel.OnPlayerDeath -= PlayerDead;
        }

        public async void FinalBossDead(IRespawnable respawnable)
        {
            SaveParametres();
            await AudioService.Instance.FadeOutPlayMusic(1f);
            AudioService.Instance.PlayAudioOneShot(AudioClipNames.Winning);
            _battleScreen.HideRespawnButton();
            _battleScreen.SetGameOverText(YOU_WIN);
            _battleScreen.ShowGameOverPanel();
            _pausePresenter.PauseOn();
        }

        private void QuitGame()
        {
            OnGameEnded?.Invoke();
            SaveParametres();
            _pausePresenter.PauseOff();
            SceneManager.LoadScene(ConstantsProvider.MAIN_MENU_SCENE_INDEX);
        }

        private async void PlayerDead()
        {
            await AudioService.Instance.FadeOutPlayMusic(1f);
            AudioService.Instance.PlayAudioOneShot(AudioClipNames.Death_Character);
            if (_playerModel.PlayerRevivesCount > 0)
            {
                _battleScreen.ShowRespawnButton();
                _playerModel.PlayerRevivesCount--;
            }
            else
            {
                _battleScreen.HideRespawnButton();
            }
            _battleScreen.ShowGameOverPanel();
            _pausePresenter.PauseOn();
        }

        private void RestartGame()
        {
            OnGameEnded?.Invoke();
            SaveParametres();
            _pausePresenter.PauseOff();
            SceneManager.LoadScene(ConstantsProvider.GAME_SCENE_INDEX);
        }

        private void SaveParametres()
        {
            _metaprogressionConfig.CollectedCoins += _runStatisticsSystemModel.CollectedCoins;
            Saver.Save(new PlayerPrefsParserModule());
            PlayerPrefs.Save();
        }
    }
}