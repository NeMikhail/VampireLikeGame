using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Interface;
using static Infrastructure.PoolSystems.Pool.GamePoolPreset;

namespace Infrastructure.PoolSystems.Pool
{
    public sealed class Pool<T> where T : MonoBehaviour, IPoolObject<T>
    {
        private readonly Dictionary<string, List<T>> _itemPool;
        private readonly Dictionary<string, Transform> _namePoolInspector;
        private Transform _root;

        private static readonly string S_PREFIX_POOL = $"[{nameof(Pool<T>)}]_";

        public GamePoolPreset Preset { get; set; }
        public Transform Root { get => _root; }

        public Pool()
        {
            _itemPool = new Dictionary<string, List<T>>();
            _namePoolInspector = new Dictionary<string, Transform>();
        }

        public Pool(GamePoolPreset gamePoolPreset)
        {
            _itemPool = new Dictionary<string, List<T>>();
            _namePoolInspector = new Dictionary<string, Transform>();
            _root = new GameObject("[" + gamePoolPreset.PoolName + "]").transform;
            Preset = gamePoolPreset;
            foreach (GamePoolItem item in Preset.PoolItems)
            {
                GameObject prefab = item.Prefab;
                int size = item.Size;
                for (int j = 0; j < size; j++)
                {
                    CashItems(prefab);
                }
            }
        }

        public T Spawn(GameObject prefab)
        {
            T result = GetItem(GetListItem(prefab.name), prefab);
            result.gameObject.SetActive(true);
            result.IsActive = true;
            return result;
        }

        private T CashItems(GameObject prefab)
        {
            T result = CreateItem(GetListItem(prefab.name), prefab);
            result.gameObject.SetActive(false);
            result.IsActive = false;
            return result;
        }

        public void DeSpawn(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.GetComponent<T>().IsActive = false;
        }

        private void SetParent(Transform transform, GameObject prefab)
        {
            DeSpawn(transform);
            transform.SetParent(_namePoolInspector[prefab.name]);
        }

        public void ClearPool()
        {
            _itemPool.Clear();
        }

        private List<T> GetListItem(string type)
        {
            if (_itemPool.TryGetValue(type, out List<T> item))
            {
                return item;
            }

            _namePoolInspector[type] = new GameObject(S_PREFIX_POOL + type).transform;
            _namePoolInspector[type].SetParent(_root);
            return _itemPool[type] = new List<T>();
        }

        private T GetItem(List<T> items, GameObject prefab)
        {
            if (!FirstItem(items, out T enemy))
            {
                T item = prefab.GetComponent<T>();
                T instantiate = Object.Instantiate(item);
                SetParent(instantiate.transform, prefab);
                items.Add(instantiate);
                GetItem(items, prefab);
            }
            FirstItem(items, out enemy);
            return enemy;
        }

        private T CreateItem(List<T> items, GameObject prefab)
        {
            T item = prefab.GetComponent<T>();
            T instantiate = Object.Instantiate(item);
            SetParent(instantiate.transform, prefab);
            items.Add(instantiate);
            DeSpawn(instantiate.transform);
            return item;
        }

        private bool FirstItem(List<T> items, out T res)
        {
            foreach (T item in items)
            {
                if (item.gameObject.activeSelf) continue;
                res = item;
                return true;
            }
            res = null;
            return false;
        }
    }
}