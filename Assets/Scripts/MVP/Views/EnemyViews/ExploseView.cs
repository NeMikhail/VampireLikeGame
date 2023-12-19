using System;
using System.Collections;
using UnityEngine;
using Core.Interface.IFeatures;

namespace MVP.Views.EnemyViews
{
    internal sealed class ExploseView : MonoBehaviour, IExplosive, IAttackable
    {
        [SerializeField] private Transform _parent;

        public Action OnExplode { get; set; }

        public float Radius
        {
            get
            {
                return transform.localScale.x / 2f;
            }
            set
            {
                float _value = value * 2;
                transform.localScale = new Vector3(_value, _value, _value);
            }
        }
        public float Damage { get; set; }

        public void Explode()
        {
            if (_parent.gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                StartCoroutine(Explosive());
            }
        }

        private IEnumerator Explosive()
        {
            _parent.position += new Vector3(0, -5, 0);

            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);
            OnExplode!.Invoke();
        }
    }
}