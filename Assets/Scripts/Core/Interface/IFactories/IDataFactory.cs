using MVP.Models.EnemyModels;
using Core.Interface.IModels;
using Configs;

namespace Core.Interface.IFactories
{
    internal interface IDataFactory
    {
        public void InitialiseData();
        public EnemyModel CreateEnemyModel(EnemyScriptableObject enemyScriptableObject, IModifiersModel modifiersModel);
        public EnemyBulletModel CreateEnemyBulletModel(EnemyBulletConfig enemyBulletScriptableObject);
        public void CreateWeaponModel(WeaponScriptableObject weaponScriptableObject);
        public void CreatePassiveItemModel(PassiveItemsScriptableObject passiveItemScriptableObject);
        public ILocationObjectModel CreateLocationObjectModel();
    }
}