using System;
using Configs.LevelUpConfig;
using Core;
using Core.Interface.IModels;
using Core.ResourceLoader;
using Structs;

namespace MVP.Models.LevelModel
{
    internal sealed class ExpirienceLevelModel : IExpirienceLevelModel
    {
        private const int DEFAULT_START_LEVEL_VALUE_EXPERIENCE = 0;
        private const int NULL = 0;
        private const int DEFAULT_EXPIRIENCE_VALUE = 0;
        private const int SMALL_EXPIRIENCE_VALUE = 1;
        private const int MEDIUM_EXPIRIENCE_VALUE = 2;
        private const int LARGE_EXPIRIENCE_VALUE = 5;

        private int _currentLevelValue;  //lvl XP
        private int _maxLevelValue;
        private int _currentLevelNumber;
        private IPlayerModel _playerModel;
        private LevelUpConfig _levelUpConfig;

        public event Action<int> OnLevelValueChange;
        public event Action<int> OnLevelNumberChanged;
        public event Action<int, int> OnFirstLevelUpdateExperience;

        public int CurrentLevelNumber => _currentLevelNumber;

        public ExpirienceLevelModel(IPlayerModel playerModel)
        {
            _levelUpConfig = ResourceLoadManager.GetConfig<LevelUpConfig>();
            SetDefaultParameters();
            _playerModel = playerModel;
        }


        public void Initialize()
        {
            OnFirstLevelUpdateExperience?.Invoke(_maxLevelValue, _currentLevelValue);
            OnLevelValueChange?.Invoke(_currentLevelValue);
            OnLevelNumberChanged?.Invoke(_currentLevelNumber);
        }

        public void AddValueExperience(TypeOfExperience typeOfExperience)
        {
            int value;
            switch (typeOfExperience)
            {
                case TypeOfExperience.Small:
                    value = (int)(SMALL_EXPIRIENCE_VALUE * _playerModel.PlayerExpirienceMultiplier);
                    break;
                case TypeOfExperience.Medium:
                    value = (int)(MEDIUM_EXPIRIENCE_VALUE * _playerModel.PlayerExpirienceMultiplier);
                    break;
                case TypeOfExperience.Large:
                    value = (int)(LARGE_EXPIRIENCE_VALUE * _playerModel.PlayerExpirienceMultiplier);
                    break;
                default:
                    value = DEFAULT_EXPIRIENCE_VALUE;
                    break;
            }

    
            int remains = (_currentLevelValue + value) - _maxLevelValue;

            if (remains >= NULL)
            {
                _currentLevelValue = remains;

                UpdateLevelNumber();
                _maxLevelValue = SetCurrentXPIncreaser();

                OnFirstLevelUpdateExperience?.Invoke(_maxLevelValue, _currentLevelValue);
                return;
            }

            _currentLevelValue += value;
            OnLevelValueChange?.Invoke(_currentLevelValue);
        }

        private void UpdateLevelNumber()
        {
            _currentLevelNumber++;
            OnLevelNumberChanged?.Invoke(_currentLevelNumber);
        }

        private int SetCurrentXPIncreaser()
        {
            if (_currentLevelNumber < 20)
                return CalculateXPRequirements(_levelUpConfig.InitialXPIncrease);
            else if (_currentLevelNumber > 20 && _currentLevelNumber < 40)
                return CalculateXPRequirements(_levelUpConfig.MediumXPIncrease);
            else if (_currentLevelNumber > 40)
                return CalculateXPRequirements(_levelUpConfig.FinalXPIncrease);

            return SetBorderXPIncreaser();
        }

        private int SetBorderXPIncreaser()
        {
            return _currentLevelNumber switch
            {
                20 => _levelUpConfig.FirstBorderRequirement,
                40 => _levelUpConfig.SecondBorderRequirement,
                _ => 0,
            };
        }

        private int CalculateXPRequirements(int increaserValue) =>
            _levelUpConfig.StartXPRequirement + (_currentLevelNumber - 1) * increaserValue;

        public void Reset()
        {
            SetDefaultParameters();
            Initialize();   
        }

        private void SetDefaultParameters()
        {
            _currentLevelNumber = ConstantsProvider.DEFAULT_LEVEL_NUMBER;
            _maxLevelValue = SetCurrentXPIncreaser();
            _currentLevelValue = DEFAULT_START_LEVEL_VALUE_EXPERIENCE;
        }
    }
}