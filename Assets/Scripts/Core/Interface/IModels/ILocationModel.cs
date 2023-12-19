using Configs;

namespace Core.Interface.IModels
{
    internal interface ILocationModel
    {
        LocationDescriptor CurrentLocation { get; }
    }
}