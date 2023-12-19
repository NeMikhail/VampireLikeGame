using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.ISynergy;

namespace Core
{
    internal sealed class DataProvider : IDataProvider
    {
        private IPlayerModel _playerModel;
        private IExpirienceLevelModel _levelModel;
        private IModifiersModel _modifiersModel;
        private IRadiusExperiencePickModel _radiusExperiencePickModel;
        private List<IWeaponModel> _weaponModelsList = new List<IWeaponModel>();
        private List<IPassiveItemModel> _passiveItemModelsList = new List<IPassiveItemModel>();
        private ILocationModel _locationModel;
        private IRunStatisticsSystemModel _runStatisticsModel;
        private IAchievementModel _achievementModel;
        private ISynergyModel _synergyModel;

        public IPlayerModel PlayerModel
        {
            get { return _playerModel; }
        }
        public IRunStatisticsSystemModel RunStatisticsModel
        {
            get { return _runStatisticsModel; }
        }
        public IExpirienceLevelModel LevelModel
        {
            get { return _levelModel; }
        }
        public IRadiusExperiencePickModel RadiusExperiencePickModel
        {
            get { return _radiusExperiencePickModel; }
        }
        public IModifiersModel ModifiersModel
        {
            get { return _modifiersModel; }
        }
        public List<IWeaponModel> WeaponModelsList
        {
            get { return _weaponModelsList; }
        }
        public List<IPassiveItemModel> PassiveItemModelsList
        {
            get { return _passiveItemModelsList; }
        }
        public ILocationModel LocationModel
        {
            get { return _locationModel; }
        }
        public ISynergyModel SynergyModel
        {
            get { return _synergyModel; }
        }

        public IAchievementModel AchievementModel
        {
            get { return _achievementModel; }
        }


        public void AddSynergyModel(ISynergyModel synergyModel)
        {
            _synergyModel = synergyModel;
        }
		
        public void AddPlayerModel(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void AddRunStatisticsModel(IRunStatisticsSystemModel runStatisticsModel)
        {
            _runStatisticsModel = runStatisticsModel;
        }

        public void AddLevelModel(IExpirienceLevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void AddModifiersModel(IModifiersModel modifiersModel)
        {
            _modifiersModel = modifiersModel;
        }

        public void AddWeaponModel(IWeaponModel weaponModel)
        {
            _weaponModelsList.Add(weaponModel);
        }

        public void AddPassiveItemModel(IPassiveItemModel passiveItemModel)
        {
            _passiveItemModelsList.Add(passiveItemModel);
        }

        public void AddLocationModel(ILocationModel locationModel)
        {
            _locationModel = locationModel;
        }

        public void AddAchievementModel(IAchievementModel achievementModel)
        {
            _achievementModel = achievementModel;
        }

        public IWeaponModel GetWeaponModel(int index)
        {
            if (index < GetCurrentWeponsListCount())
            {
                IWeaponModel weaponModel = _weaponModelsList[index];
                return weaponModel;
            }
            Debug.Log("Trying to get null WeaponModel");
            return null;
        }

        public IPassiveItemModel GetPassiveItemModel(int index)
        {
            if (index < GetPassiveItemsListCount())
            {
                IPassiveItemModel model = PassiveItemModelsList[index];
                return model;
            }
            Debug.Log("Trying to get null PassiveItemModel");
            return null;
        }

        public int GetCurrentWeponsListCount()
        {
            return _weaponModelsList.Count;
        }

        public int GetPassiveItemsListCount()
        {
            return _passiveItemModelsList.Count;
        }

        public void AddRadiusExperiencePickModel(IRadiusExperiencePickModel radiusExperiencePickModel)
        {
            _radiusExperiencePickModel = radiusExperiencePickModel;
        }
    }
}