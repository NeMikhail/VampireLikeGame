using Core.Interface.IFeatures;
using Core.Interface.IViews;
using System;
using UnityEngine;

namespace MVP.Views.LocationObjectViews
{
    public class LocationObjectView : MonoBehaviour, ILocationObjectView
    {
        [SerializeField] private GameObject _locationObject;
        [SerializeField] private Transform _transform;
        [SerializeField] private Collider _collider;

        [SerializeField] private bool _isCamVisible;

        public bool IsCamVisible => _isCamVisible;

        public GameObject GameObject => _locationObject;        
        public Transform Transform => _transform;
        public Collider Collider => _collider;
            
        public event Action<IDamageable> OnStop;
        public event Action<float> OnDamage;
        public event Action<ILocationObjectView> OnBreaking;


        public void CauseDamage(float damage)
        {
            OnDamage?.Invoke(damage);
        }

        public void Break()
        {
            GameObject.SetActive(false);
            OnBreaking?.Invoke(this);
        }

        private void OnDisable()
        {
            OnStop?.Invoke(this); 
        }
    }   
}