using UnityEngine;

namespace MVP.Views.EnemyViews
{
    internal class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Transform _barHealthTransform;
        private float _defaultSize;

        private void Awake()
        {
            _defaultSize = _barHealthTransform.localScale.x;
        }

        public void SetHealth(float healthRatio)
        {
            Vector3 localScale = _barHealthTransform.localScale;
            localScale.x = healthRatio * _defaultSize;
            _barHealthTransform.localScale = localScale;
        }
    }
}
