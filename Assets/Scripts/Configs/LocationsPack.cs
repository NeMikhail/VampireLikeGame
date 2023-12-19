using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLocationsPack", menuName = "Configs/Location/NewLocationPack")]
    internal sealed class LocationsPack : ScriptableObject
    {
        [SerializeField] private LocationDescriptor _currentLocationDescriptor;
        public bool HasBeenChanged;

        public LocationDescriptor CurrentLocation
        {
            get => _currentLocationDescriptor;
            set => _currentLocationDescriptor = value;
        }
        public LocationDescriptor[] LocationDescriptors;
    }
}