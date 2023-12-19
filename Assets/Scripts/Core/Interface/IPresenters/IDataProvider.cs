using System.Collections.Generic;
using Core.Interface.IModels;
using Core.Interface.ISynergy;

namespace Core.Interface.IPresenters
{
    internal interface IDataProvider : IPresenter
    {
        public IPlayerModel PlayerModel { get; }
        public IRunStatisticsSystemModel RunStatisticsModel { get; }
        public IExpirienceLevelModel LevelModel { get; }
        public IModifiersModel ModifiersModel { get; }
        public IRadiusExperiencePickModel RadiusExperiencePickModel { get; }
        public List<IWeaponModel> WeaponModelsList { get; }
        public List<IPassiveItemModel> PassiveItemModelsList { get; }
        public ILocationModel LocationModel { get; }
        public IAchievementModel AchievementModel { get; }
        public ISynergyModel SynergyModel { get; }

        public void AddPlayerModel(IPlayerModel playerModel);
        public void AddRunStatisticsModel(IRunStatisticsSystemModel runStatisticsModel);
        public void AddLevelModel(IExpirienceLevelModel playerModel);
        public void AddModifiersModel(IModifiersModel modifiersModel);
        public void AddWeaponModel(IWeaponModel weaponModel);
        public void AddPassiveItemModel(IPassiveItemModel passiveItemModel);
        public void AddLocationModel(ILocationModel locationModel);
        public IWeaponModel GetWeaponModel(int index);
        public IPassiveItemModel GetPassiveItemModel(int index);
        public int GetCurrentWeponsListCount();
        public int GetPassiveItemsListCount();
        public void AddRadiusExperiencePickModel(IRadiusExperiencePickModel radiusExperiencePickModel);
        public void AddAchievementModel(IAchievementModel achievementModel);
        public void AddSynergyModel(ISynergyModel synergyModel);
    }
}