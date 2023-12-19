using UnityEngine;
using Configs;
using Core.Interface;
using Core.ResourceLoader;

using MVP.Views.LocationObjectViews;

namespace Core.LocationObjectsSystem
{
    public sealed class LocationObjectSpawner
    {
        private readonly Transform _parent;
        private readonly LocationObjectView _locationObjectPrefab;
        private readonly IPositionMaker _positionMaker;

        public LocationObjectSpawner(LocationObjectView locationObjectPrefab)
        {
            _locationObjectPrefab = locationObjectPrefab;

            LocationGeneratorConfig locationGeneratorConfig = ResourceLoadManager.GetConfig<LocationsPack>().CurrentLocation.
                LocationGeneratorConfig;
            int half = (locationGeneratorConfig.LocationSize - 1) / 2 * ConstantsProvider.TILE_SIZE;
            var maxPoint = new Vector3(half, 0.0f, half);

            _positionMaker = new RandomFloatPositionMaker(-maxPoint, maxPoint);
            _parent = new GameObject("LocationObjects").transform;
        }


        public LocationObjectView Spawn()
        {
            Vector3 position = _positionMaker.GetPoint();
            return Object.Instantiate(_locationObjectPrefab, position, Quaternion.identity, _parent);
        }
    }
}