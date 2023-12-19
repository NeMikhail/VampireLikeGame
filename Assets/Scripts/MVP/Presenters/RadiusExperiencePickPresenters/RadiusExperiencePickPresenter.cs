using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface;

namespace MVP.Presenters.RadiusExperiencePickPresenters
{
    internal class RadiusExperiencePickPresenter : IInitialisation, ICleanUp
    {
        private IRadiusPickUpdatable _radiusPickUpdatableModel;
        private IValueSatable _radiusOfPickingUpSphereView;
        private IPlayerModel _playerModel;
        public RadiusExperiencePickPresenter(IRadiusPickUpdatable radiusPickUpdatable, IValueSatable setRadiusOfExperiencePick,
            IPlayerModel playerModel)
        {
            _radiusPickUpdatableModel = radiusPickUpdatable;
            _radiusOfPickingUpSphereView = setRadiusOfExperiencePick;
            _playerModel = playerModel;
        }

        public void Initialisation()
        {
            _radiusPickUpdatableModel.OnRadiusChanged += _radiusOfPickingUpSphereView.SetValue;
            (_radiusPickUpdatableModel as IResetable).Reset();
            _playerModel.PickupRangeChanged += ChangePickupRadius;
        }

        private void ChangePickupRadius()
        {
            (_radiusPickUpdatableModel as IResetable).Reset();
        }

        public void Cleanup()
        {
            _radiusPickUpdatableModel.OnRadiusChanged -= _radiusOfPickingUpSphereView.SetValue;
            _playerModel.PickupRangeChanged -= ChangePickupRadius;
        }
    }
}
