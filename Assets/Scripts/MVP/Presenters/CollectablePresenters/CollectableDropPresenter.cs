using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Interface.IViews;
using Core.ResourceLoader;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.CollectableViews;

namespace MVP.Presenters.CollectablePresenters
{
    internal sealed class CollectableDropPresenter
    {
        private List<CollectableView> _collectableViewList;
        private GamePoolPreset _collectablePoolPreset;

        public CollectableDropPresenter()
        {
            _collectableViewList = new List<CollectableView>();
            _collectablePoolPreset = ResourceLoadManager.GetConfig<GamePoolPreset>(ConstantsProvider.
                NAME_COLLECTABLE_POOL_PRESET);

            foreach (GamePoolPreset.GamePoolItem poolItem in _collectablePoolPreset.PoolItems)
            {
                CollectableView collectableView = poolItem.Prefab.GetComponent<CollectableView>();
                _collectableViewList.Add(collectableView);
            }
        }

        public void DropCollectable(ILocationObjectView locationObject)
        {
            int index = (int)Random.Range(0, _collectableViewList.Count);
            CollectableView colItem = GamePool.Collectable.Pool.Spawn(_collectableViewList[index].CollectablePrefab);
            float objectHeight = colItem.GetComponent<Renderer>().bounds.size.y;

            colItem.transform.SetPositionAndRotation(locationObject.Transform.position + new Vector3(0, objectHeight /
                2f, 0), locationObject.Transform.rotation);
            locationObject.OnBreaking -= DropCollectable;
        }
    }
}