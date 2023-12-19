using Configs;
using Core.Interface.IModels;

namespace MVP.Models.LocationModel
{
    internal sealed class LocationModel : ILocationModel
    {
        private LocationsPack _locationConfig;
        private LocationDescriptor[] _locations;

        public LocationDescriptor CurrentLocation
        {
            get => _locationConfig.CurrentLocation; 
            private set => _locationConfig.CurrentLocation = value;
        }

        public LocationModel(LocationsPack scriptableLocationCfg)
        {
            _locationConfig = scriptableLocationCfg;
            _locations = scriptableLocationCfg.LocationDescriptors;
            Initialize();
        }


        private void Initialize()
        {
            if (_locations.Length != 0 && !_locationConfig.HasBeenChanged)
            {
                CurrentLocation = _locations[0];
            }
        }
    }
}