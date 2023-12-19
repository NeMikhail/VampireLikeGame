using Configs;
using static Configs.WeaponScriptableObject;
using static Configs.PassiveItemsScriptableObject;
using Core.Interface.IFactories;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.ISynergy;
using Core.ResourceLoader;
using MVP.Models.LevelModel;
using MVP.Models.PlayerModels;
using MVP.Models.RadiusExperiencePickModel;
using MVP.Models.EnemyModels;
using MVP.Models.WeaponModels;
using MVP.Models.LocationModel;
using MVP.Models.LocationObjectModel;
using MVP.Models.PassiveItemModels;
using MVP.Models.SynergyModels;

namespace Core
{
    internal sealed class DataFactory : IDataFactory
    {
        private IDataProvider _dataProvider;

        internal DataFactory(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


        public void InitialiseData()
        {
            PlayerModel playerModel = CreatePlayerModel();
            _dataProvider.AddPlayerModel(playerModel);

            IExpirienceLevelModel levelModel = CreateLevelModel();
            _dataProvider.AddLevelModel(levelModel);

            IModifiersModel modifiersModel = CreateModifiersModel();
            _dataProvider.AddModifiersModel(modifiersModel);

            IRadiusExperiencePickModel experiencePickModel = CreateExperiencePickModel();
            _dataProvider.AddRadiusExperiencePickModel(experiencePickModel);

            LocationModel locationModel = CreateLocationModel();
            _dataProvider.AddLocationModel(locationModel);

            ISynergyModel synergyModel= CreateSynergyModel();
            _dataProvider.AddSynergyModel(synergyModel);
        }

        public EnemyModel CreateEnemyModel(EnemyScriptableObject enemyScriptableObject, IModifiersModel modifiersModel)
        {
            var enemyModel = new EnemyModel(enemyScriptableObject.EnemySpeed, enemyScriptableObject.EnemyHealth,
                enemyScriptableObject.EnemyDamage, enemyScriptableObject.EnemyType, enemyScriptableObject.TypeOfExperience,
                modifiersModel);
            return enemyModel;
        }

        public EnemyBulletModel CreateEnemyBulletModel(EnemyBulletConfig enemyBulletScriptableObject)
        {
            var enemyBulletModel = new EnemyBulletModel(enemyBulletScriptableObject.Interval,
                enemyBulletScriptableObject.Speed, enemyBulletScriptableObject.Prefab);
            return enemyBulletModel;
        }

        public void CreateWeaponModel(WeaponScriptableObject weaponScriptableObject)
        {
            foreach (WeaponAttributes item in weaponScriptableObject.Items)
            {
                var weaponModel = new WeaponModel(item);
                _dataProvider.AddWeaponModel(weaponModel);
            }
        }

        public void CreatePassiveItemModel(PassiveItemsScriptableObject passiveItemScriptableObject)
        {
            foreach (PassiveItemAttributes item in passiveItemScriptableObject.Items)
            {
                var passiveItemModel = new PassiveItemModel(item);
                _dataProvider.AddPassiveItemModel(passiveItemModel);
            }
        }

        public ISynergyModel CreateSynergyModel()
        {
            return new SynergyModel();
        }

        public ILocationObjectModel CreateLocationObjectModel()
        {
            return new LocationObjectModel(ResourceLoadManager.GetConfig<LocationObjectConfig>());
        }

        public LocationModel CreateLocationModel()
        {
            return new LocationModel(ResourceLoadManager.GetConfig<LocationsPack>());
        }

        private PlayerModel CreatePlayerModel()
        {
            return new PlayerModel(ResourceLoadManager.GetConfig<PlayerScriptableConfig>());
        }

        private IExpirienceLevelModel CreateLevelModel()
        {
            return new ExpirienceLevelModel(_dataProvider.PlayerModel);
        }

        private IRadiusExperiencePickModel CreateExperiencePickModel()
        {
            return new RadiusExperiencePickModel(_dataProvider.PlayerModel);
        }

        private IModifiersModel CreateModifiersModel()
        {
            return new ModifiersModel(ResourceLoadManager.GetConfig<LocationsPack>(),
                ResourceLoadManager.GetConfig<UpgradeModifiersConfig>(), ResourceLoadManager.GetConfig<PlayerScriptableConfig>());
        }
    }
}