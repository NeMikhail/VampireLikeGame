using UnityEngine;

namespace Core.Interface.IViews
{
    internal interface IEnemyView : IView
    {
        public GameObject EnemyObject { get; set; }
        public Transform EnemyTransform { get; set; }
    }
}