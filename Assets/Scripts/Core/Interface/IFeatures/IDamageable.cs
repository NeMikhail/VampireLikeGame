using System;
using Core.Interface.IModels;
using MVP.Views.WeaponViews;

namespace Core.Interface.IFeatures
{
    public interface IDamageable
    {
        public bool IsCamVisible { get; }

        public event Action<IDamageable> OnStop;

        public void CauseDamage(float damage);
        public void CauseKnockback(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile) { }
    }
}