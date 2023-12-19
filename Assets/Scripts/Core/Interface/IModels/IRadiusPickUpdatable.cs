using System;

namespace Core.Interface.IModels
{
    internal interface IRadiusPickUpdatable
    {
        event Action<float> OnRadiusChanged;
    }
}
