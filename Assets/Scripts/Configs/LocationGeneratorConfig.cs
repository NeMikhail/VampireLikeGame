using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLocationGeneration", menuName = "Configs/Location/LocationGeneration")]
    public class LocationGeneratorConfig : ScriptableObject
    {
        [SerializeField] private int _locationSize;
        [SerializeField] private List<GameObject> _tilesPrefabs;
        [SerializeField] private List<int> _tileIndexes;

        public int LocationSize => _locationSize;
        public List<GameObject> TilesPrefabs => _tilesPrefabs;
        public List<int> TileIndexes => _tileIndexes;
    }
}