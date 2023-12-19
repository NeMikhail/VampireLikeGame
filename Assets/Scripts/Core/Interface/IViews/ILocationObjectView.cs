using System;
using UnityEngine;
using Core.Interface.IFeatures;

namespace Core.Interface.IViews
{
    public interface ILocationObjectView : IDamageable
    {
        //logically, event "OnDamage" should be relocated into the IDamageable
        public event Action<float> OnDamage;
        public event Action<ILocationObjectView> OnBreaking;
        public Transform Transform { get; }
        public void Break();
    }
}