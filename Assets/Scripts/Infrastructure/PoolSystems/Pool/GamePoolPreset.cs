using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.PoolSystems.Pool
{
    [CreateAssetMenu(fileName = "GamePoolPreset", menuName = "GamePool/GamePoolPreset", order = 1)]
    public sealed class GamePoolPreset : ScriptableObject
    {
        [SerializeField] private string _poolName;
        [SerializeField] private List<GamePoolItem> _poolItems = new List<GamePoolItem>();
        public string PoolName { get => _poolName; }
        public List<GamePoolItem> PoolItems { get => _poolItems; set => _poolItems = value; }

        [Serializable]
        public sealed class GamePoolItem
        {
            [SerializeField] private GameObject prefab;
            [SerializeField] private int size;

            public GameObject Prefab => prefab;
            public int Size => size;
        }
    }
}
