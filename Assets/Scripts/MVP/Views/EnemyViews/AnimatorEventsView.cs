using System;
using UnityEngine;

namespace MVP.Views.EnemyViews
{
    public sealed class AnimatorEventsView : MonoBehaviour
    {
        public event Action OnShoot;

        private void Shoot()
        {
            OnShoot?.Invoke();
        }
    }
}

