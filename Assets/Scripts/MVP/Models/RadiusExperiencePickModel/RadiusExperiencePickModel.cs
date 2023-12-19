using Core.Interface.IModels;
using System;

namespace MVP.Models.RadiusExperiencePickModel
{
    internal class RadiusExperiencePickModel: IRadiusExperiencePickModel
    {
        private IPlayerModel _playerModel;
        private float _radius;

        public event Action<float> OnRadiusChanged;

        public RadiusExperiencePickModel(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            _radius = _playerModel.PlayerExpiriencePickupRange;
        }

        public void UpdateRadius(float radius)
        {
            _radius = radius;
            OnRadiusChanged?.Invoke(radius);
        }

        public void Reset()
        {
            UpdateRadius(_playerModel.PlayerExpiriencePickupRange);
        }

    }
}
