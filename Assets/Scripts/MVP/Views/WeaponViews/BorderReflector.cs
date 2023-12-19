using UnityEngine;

namespace MVP.Views.WeaponViews
{
    internal sealed class BorderReflector : MonoBehaviour
    {
        [SerializeField] private bool isLeftRightBorder;

        private Vector3 _normalVector;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "BounceProjectile")
            {
                if (other.TryGetComponent<ProjectileView>(out ProjectileView projectile))
                {
                    if (!isLeftRightBorder)
                        _normalVector = new Vector3(0f, 0f, 1f);
                    else 
                        _normalVector = new Vector3(1f, 0f, 0f);

                    projectile.ChangeBounceCurrentCount();

                    var reflectVector = Vector3.Reflect(projectile.RigidBodyObj.velocity, _normalVector);
                    projectile.Direction = reflectVector;
                }
            }
        }
    }
}