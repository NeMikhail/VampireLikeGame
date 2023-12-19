using UnityEngine;

namespace MVP.Models.EnemyModels
{
    public class EnemyBulletModel
    {
        private float _interval;
        private float _speed;
        private GameObject _prefab;

        public float Interval
        {
            get => _interval;
            set => _interval = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        public EnemyBulletModel(float interval, float speed, GameObject prefab)
        {
            _interval = interval;
            _speed = speed;
            _prefab = prefab;
        }
    }
}