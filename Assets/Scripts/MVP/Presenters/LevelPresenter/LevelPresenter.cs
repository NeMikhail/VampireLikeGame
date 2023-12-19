using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using System;
using UnityEngine;

namespace MVP.Presenters.LevelPresenter
{
    internal class LevelPresenter : IInitialisation, ICleanUp
    {
        private ICharacterLevelView _characterLevelView;
        private IExperienceView _experienceView;
        private IExpirienceLevelModel _levelModel;

        internal LevelPresenter(IViewProvider levelView, IExpirienceLevelModel levelModel)
        {
            _characterLevelView = levelView.GetCharacterLevelView();
            _experienceView = levelView.GetExperienceIndicator();
            _levelModel = levelModel;


            _levelModel.OnLevelValueChange += _experienceView.SetCurrentValue;
            _levelModel.OnLevelNumberChanged += _characterLevelView.SetLevel;
            _levelModel.OnFirstLevelUpdateExperience += _experienceView.SetValues;
        }

        public void Initialisation()
        {
            _levelModel.Initialize();
        }

        public void Cleanup()
        {
            _levelModel.OnLevelValueChange -= _experienceView.SetCurrentValue;
            _levelModel.OnLevelNumberChanged -= _characterLevelView.SetLevel;
            _levelModel.OnFirstLevelUpdateExperience -= _experienceView.SetValues;
        }
    }
}
