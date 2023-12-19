using Core.Interface.IModels;
using Enums;
using static Configs.AchievementListConfig;

namespace Core.AchievementSystem.AchievementUnlockers
{
    internal sealed class EnemyKilledUnlocker : BaseUnlocker
    {
        private WeaponsName _weaponsName;

        public EnemyKilledUnlocker(Achievement enemyAchiv, IRunStatisticsSystemModel stats) : base(enemyAchiv, stats)
        {
            _weaponsName = enemyAchiv.WeaponsName;
        }


        protected override void AssignValueToCompare()
        {
            if (_weaponsName == WeaponsName.None && AchievementData.EnemyType == EnemyType.None)
            {
                AchievementData.CurrentValue += _statistics.TotalKills;
                _valueToCompare = AchievementData.CurrentValue;
            }

            if (_weaponsName != WeaponsName.None && _statistics.KilledByWeapon.ContainsKey(_weaponsName))
            {
                AchievementData.CurrentValue += _statistics.KilledByWeapon[_weaponsName];
                _valueToCompare = AchievementData.CurrentValue;
            }
            else if (!_statistics.KilledByWeapon.ContainsKey(_weaponsName)) return;
        }
    }
}