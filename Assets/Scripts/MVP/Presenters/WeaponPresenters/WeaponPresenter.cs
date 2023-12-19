using System;
using System.Collections.Generic;
using Core.Interface;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;
using Enums;
using MVP.Models.PassiveItemModels;
using MVP.Models.StatisticsSystemModels;
using MVP.Models.WeaponModels.AttackModelsWeapon;
using MVP.Presenters.PlayerPresenters;
using MVP.Views.PlayerViews;
using UI;

namespace MVP.Presenters.WeaponPresenters
{
    internal sealed class WeaponPresenter : IInitialisation, IExecute, ICleanUp, IPresenter
    {
        private IPlayerModel _playerModel;
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private PlayerView _playerView;
        private HealthChangingPresenter _healthChangingPresenter;
        private DamagePopupPresenter _damagePopupPresenter;
        private IDataProvider _dataProvider;
        private UIStatisticView _statisticsView;
        private Dictionary<WeaponType, IAttackWeapon> _attackWeapon = new();
        private List<WeaponCard> _weaponCards;
        private List<int> _weaponCardKillsCount;

        public WeaponPresenter(IDataProvider dataProvider, PlayerView playerView, HealthChangingPresenter healthChanging,
            IViewProvider viewProvider, DamagePopupPresenter damagePopupPresenter)
        {
            _playerModel = dataProvider.PlayerModel;
            _healthChangingPresenter = healthChanging;
            _damagePopupPresenter = damagePopupPresenter;
            _playerModel.OnAddNewWeapon += AddNewWeapon;
            _playerModel.OnRemoveWeapon += RemoveWeapon;
            _playerModel.OnAddNewPassiveItem += AddNewPassiveItem;
            _playerModel.OnRemovePassiveItem += RemovePassiveItem;
            _healthChangingPresenter.OnTakeHealth += _damagePopupPresenter.CreateDamagePopup;
            _playerView = playerView;
            _dataProvider = dataProvider;
            _runStatisticsSystemModel = _dataProvider.RunStatisticsModel;
            _statisticsView = viewProvider.GetStatistiсView();
            _statisticsView.OnShowStatistic += ShowStatistic;
            _weaponCards = new List<WeaponCard>();
            _weaponCardKillsCount = new List<int>();
        }


        public void Initialisation()
        {
            _attackWeapon.Add(WeaponType.RandomDamage, new BlowFromHeaveAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.AreaAroundPlayer, new AreaDamageAttackWeapon(_playerView, _healthChangingPresenter));
            _attackWeapon.Add(WeaponType.RotatingProjectiles, new RotatingProjectileAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.WhipProjectiles, new WhipProjectileAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.ScatteredProjectile, new FireProjectileAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.ClosestEnemyProjectiles, new ClosestEnemyAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.DirectionalProjectiles, new DirectionalProjectileAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.BounceProjectiles, new BounceProjectileAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.SinergyRandomDamage, new SinergyBlowFromHeaveAttackWeapon(_playerView));
            _attackWeapon.Add(WeaponType.SinergyBounceProjectiles, new BounceProjectileAttackWeapon(_playerView));
            _playerModel.AddWeapon(_dataProvider.GetWeaponModel((int)_playerModel.PlayerDefaultWeapon));

            SubscribeToDamagePopups();
        }

        public void Execute(float deltaTime)
        {
            foreach (IWeaponModel weapon in _playerModel.WeaponList)
            {
                _attackWeapon[weapon.WeaponType].Execute(weapon);
            }
        }

        private void ShowStatistic()
        {
            var ts = TimeSpan.FromSeconds(_runStatisticsSystemModel.TotalTime);
            var globalText = $"Coins: {_runStatisticsSystemModel.CollectedCoins}\n" +
                $"Location Name: {_runStatisticsSystemModel.LocationName}\n" +
                $"Total Time: {ts.Minutes}:{ts.Seconds}\n" +
                $"Total Kills: {_runStatisticsSystemModel.TotalKills}";
            _weaponCards.Clear();
            _weaponCardKillsCount.Clear();

            foreach (KeyValuePair<WeaponCard, int> killed in _runStatisticsSystemModel.KilledByWeaponCard)
            {
                _weaponCards.Add(killed.Key);
                _weaponCardKillsCount.Add(killed.Value);
            }
            _statisticsView.SetStatistic(globalText, _weaponCards, _weaponCardKillsCount);
        }

        private void AddNewWeapon(IWeaponModel weapon)
        {
            _attackWeapon[weapon.WeaponType].Init(weapon);
            weapon.OnKill += _runStatisticsSystemModel.AddWeaponInStatistics;
            weapon.OnLevelUp += CheckConditionsSinergy;
            CheckConditionsSinergy();
        }

        private void RemoveWeapon(IWeaponModel weapon)
        {
            weapon.DeActivateWeapon();
            weapon.OnKill -= _runStatisticsSystemModel.AddWeaponInStatistics;
            weapon.OnLevelUp -= CheckConditionsSinergy;
        }

        private void AddNewPassiveItem(IPassiveItemModel model)
        {
            model.OnLevelUp += CheckConditionsSinergy;
            CheckConditionsSinergy();
        }

        private void RemovePassiveItem(IPassiveItemModel model)
        {
            model.OnLevelUp -= CheckConditionsSinergy;
        }

        public void Cleanup()
        {
            _playerModel.OnAddNewWeapon -= AddNewWeapon;
            _playerModel.OnRemoveWeapon -= RemoveWeapon;
            _playerModel.OnAddNewPassiveItem -= AddNewPassiveItem;
            _playerModel.OnRemovePassiveItem -= RemovePassiveItem;
            _healthChangingPresenter.OnTakeHealth -= _damagePopupPresenter.CreateDamagePopup;
            UnsubscribeFromDamagePopups();
        }

        private void CheckConditionsSinergy()
        {
            List<IWeaponModel> weapons = _playerModel.WeaponList;
            for (int i = 0; i < weapons.Count; i++)
            {
                IWeaponModel weapon = weapons[i];

                if (weapon.IsSynergy && !weapon.IsAvailableSynergy && weapon.Level == weapon.MaxLevel &&
                    CheckPassiveSinergy(weapon.PassiveForSynergy))
                {
                    weapon.IsAvailableSynergy = true;

                    _playerModel.AddWeaponSinergy(weapon);
                }
            }
        }

        private bool CheckPassiveSinergy(List<PassiveItemInfo> passivesSynergy)
        {
            int count = 0;
            List<IPassiveItemModel> passivesPlayer = _playerModel.PassiveItemList;

            for (int i = 0; i < passivesSynergy.Count; i++)
            {
                PassiveItemInfo synergy = passivesSynergy[i];

                for (int j = 0; j < passivesPlayer.Count; j++)
                {
                    IPassiveItemModel player = passivesPlayer[j];
                    if (synergy.Name == player.Name && synergy.Level <= player.Level)
                        count++;
                }
            }

            if (count == passivesSynergy.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SubscribeToDamagePopups()
        {
            foreach (KeyValuePair<WeaponType, IAttackWeapon> weaponPair in _attackWeapon)
            {
                BaseAttackWeapon baseWeapon = weaponPair.Value as BaseAttackWeapon;
                baseWeapon.OnCauseDamage += _damagePopupPresenter.CreateDamagePopup;
            }
        }

        private void UnsubscribeFromDamagePopups()
        {
            foreach (KeyValuePair<WeaponType, IAttackWeapon> weaponPair in _attackWeapon)
            {
                BaseAttackWeapon baseWeapon = weaponPair.Value as BaseAttackWeapon;
                baseWeapon.OnCauseDamage -= _damagePopupPresenter.CreateDamagePopup;
            }
        }
    }
}