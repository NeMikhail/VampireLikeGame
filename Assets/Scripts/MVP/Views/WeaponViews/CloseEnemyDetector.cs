using Core.Interface.IFeatures;
using System.Collections.Generic;
using UnityEngine;

namespace MVP.Views.WeaponViews
{
    internal class CloseEnemyDetector : MonoBehaviour
    {
        private List<Transform> _closeEnemies = new List<Transform>();
        public List<Transform> CloseEnemies { get => _closeEnemies; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                CloseEnemies.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                CloseEnemies.Remove(other.transform);
            }
        }
    }
}