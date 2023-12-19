using Core.Interface.ICollectable;
using Core.Interface.IPresenters;
using Core.Interface.IViews;
using Enums;
using Infrastructure.PoolSystems.Pool;
using UnityEngine;

namespace MVP.Presenters.CollectablePresenters
{
    internal class CollectablePickingUpPresenter : IInitialisation, ICleanUp
    {
        private readonly IPlayerView _playerView;
        private readonly IAnalyze _analyzer;

        public CollectablePickingUpPresenter(IPlayerView playerView, IAnalyze analyzer)
        {
            _playerView = playerView;
            _analyzer = analyzer;
        }


        public void Initialisation()
        {
            _playerView.DetectorCollactableCollision.OnPickingUpCollectable += PickupCollectable;
        }

        private void PickupCollectable(CollectableType collectableType, Transform transform)
        {
            _analyzer.AnalyzeCollectable(collectableType);
            GamePool.Collectable.Pool.DeSpawn(transform);
        }

        public void Cleanup()
        {
            _playerView.DetectorCollactableCollision.OnPickingUpCollectable -= PickupCollectable;
        }
    }
}