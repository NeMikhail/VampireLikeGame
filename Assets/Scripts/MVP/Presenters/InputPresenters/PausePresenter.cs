using UI;
using System;
using UnityEngine;
using Core.Interface;
using Core.Interface.IInputs;
using Core.Interface.IPresenters;


namespace MVP.Presenters.InputPresenters
{
    internal sealed class PausePresenter : IInitialisation, ICleanUp
    {
        private readonly UiBattleScreen _battleScreen;
        private readonly IInputInitialisation _inputInitialisation;

        private bool _isPauseUiMenuActive;
        private bool _isPause;


        public PausePresenter(IViewProvider viewProvider, IInputInitialisation inputInitialisation)
        {
            _battleScreen = viewProvider.GetBattleScreen();
            _inputInitialisation = inputInitialisation;
        }


        public void Initialisation()
        {
            _inputInitialisation.OnPauseActivated += PauseGame;
            _battleScreen.OnMobilePauseClick += PauseGame;
            _battleScreen.OnContinueButtonClick += UnpauseGame;
        }

        private void PauseGame()
        {
            if (_isPauseUiMenuActive)
            {
                UnpauseGame();
            }
            else
            {
                if (!_isPause)
                {
                    _battleScreen.ShowPauseMenu();
                    _isPauseUiMenuActive = true;
                    PauseOn();
                }
            }
        }

        private void UnpauseGame()
        {
            _battleScreen.HidePauseMenu();
            _isPauseUiMenuActive = false;
            PauseOff();
        }

        public void PauseOn()
        {
            Time.timeScale = 0.0f;
            _isPause = true;
        }

        public void PauseOff()
        {
            Time.timeScale = 1.0f;
            _isPause = false;
        }

        public void Cleanup()
        {
            _inputInitialisation.OnPauseActivated -= PauseGame;
            _battleScreen.OnMobilePauseClick -= PauseGame;
            _battleScreen.OnContinueButtonClick -= UnpauseGame;
        }
    }
}
