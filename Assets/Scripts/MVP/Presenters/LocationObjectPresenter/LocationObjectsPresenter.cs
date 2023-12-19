using System.Collections.Generic;
using Configs;

using Core.Interface.IFactories;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.IViews;
using Core.LocationObjectsSystem;
using Core.ResourceLoader;
using MVP.Presenters.CollectablePresenters;
using MVP.Views.LocationObjectViews;

namespace MVP.Presenters.LocationObjectPresenter
{
    public sealed class LocationObjectsPresenter : IInitialisation, ICleanUp
    {
        private readonly Dictionary<ILocationObjectView, ILocationObjectModel> _locationObjects;
        private readonly CollectableDropPresenter _collectablePresenter;
        private readonly LocationObjectSpawner _locationObjectSpawner;

        internal LocationObjectsPresenter(LocationObjectSpawner spawner, IDataFactory dataFactory)
        {
            _locationObjects = new Dictionary<ILocationObjectView, ILocationObjectModel>();
            _collectablePresenter = new CollectableDropPresenter();
            _locationObjectSpawner = spawner;
            int amount = ResourceLoadManager.GetConfig<LocationsPack>().CurrentLocation.DestructableObjectsAmount;
            for (int i = 0; i < amount; i++)
            {
                LocationObjectView view = _locationObjectSpawner.Spawn();
                ILocationObjectModel model = dataFactory.CreateLocationObjectModel();
                _locationObjects.Add(view, model);
            }
        }


        public void Initialisation()
        {
            foreach (KeyValuePair<ILocationObjectView, ILocationObjectModel> locationObject in _locationObjects)
            {
                locationObject.Key.OnDamage += locationObject.Value.TakeDamage;
                locationObject.Value.OnBreaking += locationObject.Key.Break;
                locationObject.Key.OnBreaking += _collectablePresenter.DropCollectable;
            }
        }

        public void Cleanup()
        {
            foreach (KeyValuePair<ILocationObjectView, ILocationObjectModel> locationObject in _locationObjects)
            {
                locationObject.Key.OnDamage -= locationObject.Value.TakeDamage;
                locationObject.Value.OnBreaking -= locationObject.Key.Break;
                locationObject.Key.OnBreaking -= _collectablePresenter.DropCollectable;
            }           
            _locationObjects.Clear();
        }
    }
}