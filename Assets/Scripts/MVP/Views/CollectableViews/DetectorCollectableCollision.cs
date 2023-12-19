using Core.Interface.ICollectable;
using Enums;
using System;
using UnityEngine;

namespace MVP.Views.CollectableViews
{
    internal class DetectorCollectableCollision: MonoBehaviour
    {
        public event Action<CollectableType, Transform> OnPickingUpCollectable;


        private void OnTriggerEnter(Collider other)
        {
            bool success = other.gameObject.TryGetComponent<ICollectable>(out var typeOfCollectable);

            if (success)
            {
                OnPickingUpCollectable?.Invoke(typeOfCollectable.CollectableType, other.gameObject.transform);
            }
        }
    }
}