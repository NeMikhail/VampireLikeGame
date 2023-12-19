using Core;
using Core.Interface.IFeatures;
using Core.ResourceLoader;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.CollectableViews;
using UnityEngine;

namespace MVP.Presenters.ChestPresenters
{
    internal class DropChestPresenter
    {
        private readonly CollectableView _chestCollectableView;

        private const float _rareDropChance = 0.25f;
        private const float _epicDropChance = 0.1f;

        public DropChestPresenter()
        {
            _chestCollectableView = ResourceLoadManager.
                GetPrefabComponentOrGameObject<CollectableView>(ConstantsProvider.CHEST_VIEW_NAME);
        }


        public void DropChest(IRespawnable respawnable)
        {
            CollectableView chest = GamePool.Collectable.Pool.Spawn(_chestCollectableView.CollectablePrefab);
            chest.transform.SetPositionAndRotation(respawnable.RespawnableTransform.position, respawnable.RespawnableTransform.rotation);
            chest.CollectableType = RareType(Random.Range(0f, 1f));
            respawnable.OnDie -= DropChest;
        }
        
        private Enums.CollectableType RareType(float probability)
        {
            if (probability < _epicDropChance)
            {
                return Enums.CollectableType.EpicChest;
            }
            else if (probability >= _epicDropChance && probability < _epicDropChance + _rareDropChance)
            {
                return Enums.CollectableType.RareChest;
            }
            return Enums.CollectableType.CommonChest;
        }

        public void PickupChest(Transform transform)
        {
            GamePool.Collectable.Pool.DeSpawn(transform);
        }
    }
}