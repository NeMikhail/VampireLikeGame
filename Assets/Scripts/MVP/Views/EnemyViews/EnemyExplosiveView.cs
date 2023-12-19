using UnityEngine;
using Configs;
using Core.Animations.Enemy;
using MVP.Models.EnemyModels;
using MVP.Presenters.EnemyPresenters;
using MVP.Views.PlayerViews;

namespace MVP.Views.EnemyViews
{
    internal sealed class EnemyExplosiveView : EnemyView
    {
        [SerializeField] public EnemyExplosionConfig _enemyExplosionConfig;
        [SerializeField] public ExploseView _explosive;
        [SerializeField] private ParticleSystem _preExplosionArea;


        public override void Init(PlayerView player, EnemyModel enemyModel)
        {
            _enemyModel = enemyModel;
            if (_enemyAnimator != null)
            {
                var enemyAnimation = new EnemyAnimation(_enemyAnimator);
                _enemyMovement = new EnemyMovementExplosive(player, this, _enemyModel, _enemyRigidbody,
                    enemyAnimation);
            }
            else
            {
                _enemyMovement = new EnemyMovementExplosive(player, this, _enemyModel, _enemyRigidbody, null);
            }

            SetUpExplosives();
            _enemyModel.OnDie += Dead;
        }

        private void SetUpExplosives()
        {
            _explosive.gameObject.SetActive(false);
            _preExplosionArea.gameObject.SetActive(false);
            _explosive.OnExplode += Remove;
        }

        public void ShowPreExplosionArea()
        {
            float radius = _explosive.Radius * 2;
            _preExplosionArea.gameObject.transform.localScale 
                = new Vector3(radius, radius, radius);
            _preExplosionArea.gameObject.SetActive(true);
        }

        public override void Remove()
        {
            base.Remove();
            _explosive.OnExplode -= Remove;
        }
    }
}