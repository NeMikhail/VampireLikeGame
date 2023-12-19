using System;

namespace Core.Interface
{
    public interface IPoolObject<T>
    {
        public bool IsActive { get; set; }
        public event Action <T> OnSpawnObject;
        public event Action <T> OnDeSpawnObject;

        public void OnEnable();
        public void OnDisable();
    }
}
