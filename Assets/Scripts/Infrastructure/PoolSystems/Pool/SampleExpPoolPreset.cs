using System;
using System.Collections.Generic;
using UnityEngine;
using Structs;

namespace Infrastructure.PoolSystems.Pool
{
    [CreateAssetMenu(fileName = "GameExpPoolPreset", menuName = "GamePool/GameExpPoolPreset", order = 1)]
    public sealed class SampleExpPoolPreset : ScriptableObject
    {
        [SerializeField] private string _poolName;
        [SerializeField] private List<GamePoolItem> _poolItems = new List<GamePoolItem>();
        private TypeOfExperience _experienceType;
        public string PoolName { get => _poolName; }
        public List<GamePoolItem> PoolItems { get => _poolItems; set => _poolItems = value; }
        public TypeOfExperience TypeOfExperience { get => _experienceType; }

        [Serializable]
        public sealed class GamePoolItem
        {
            [SerializeField] private GameObject prefab;
            [SerializeField] private int size;
            [SerializeField] private TypeOfExperience experience;

            public GameObject Prefab => prefab;
            public int Size => size;
            public TypeOfExperience TypeOfExperience => experience;
        }
    }
}

