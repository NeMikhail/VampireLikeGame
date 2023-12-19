using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.LocationPresenter
{
    internal sealed class LocationPresenter : IInitialisation
    {
        private ILocationModel _locationModel;
        public ILocationModel LocationModel { get => _locationModel; }

        public LocationPresenter(IDataProvider dataProvider)
        {
            _locationModel = dataProvider.LocationModel;
        }

        public void Initialisation() { }
    }
}