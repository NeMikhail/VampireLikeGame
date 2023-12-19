using Core.Interface.IFeatures;
using MVP.Models.EnemyModels;
using MVP.Views.EnemyViews;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class UIBossHPBar : UiBaseScreen
    {
        [SerializeField] private Slider _bossHPBarSlider;
        private EnemyModel _bossEnemyModel;
        private float _maxBossHP;

        public void SetBossEnemyView(IRespawnable bossEnemyView)
        {
            EnemyView enemyView = (EnemyView) bossEnemyView;
            _bossEnemyModel = enemyView.EnemyModel;
            _maxBossHP = _bossEnemyModel.EnemyMaxHealth;
        }
        
        private void OnEnable()
        {
            _bossEnemyModel.OnHealthChanged += ChangeHPBarValue;
            _bossHPBarSlider.value = _bossEnemyModel.EnemyHealth / _maxBossHP;
        }

        private void ChangeHPBarValue(float currentHP)
        {
            _bossHPBarSlider.value = currentHP / _maxBossHP;
        }
        

        private void OnDisable()
        {
            _bossEnemyModel.OnHealthChanged -= ChangeHPBarValue;
        }
    }
}