using MVP.Views.WeaponViews;
using UnityEngine;

namespace Infrastructure.PoolSystems.Pool
{
    public class SamplePoolScene : MonoBehaviour
    {
        [SerializeField] private GamePoolPreset weaponParticlePreset;
        private void Awake()
        {
            GamePool.Init();
        }

        [ContextMenu("SpawnWeaponType1")]
        public void Spawn1()
        {
            Pool<ProjectileView> Pool = GamePool.Projectile.Pool;
            Pool.Spawn(Pool.Preset.PoolItems[0].Prefab);
        }

        [ContextMenu("SpawnWeaponType2")]
        public void Spawn2()
        {
            Pool<ProjectileView> Pool = GamePool.Projectile.Pool;
            Pool.Spawn(Pool.Preset.PoolItems[1].Prefab);
        }

        [ContextMenu("SpawnWeaponType3")]
        public void Spawn3()
        {
            Pool<ProjectileView> Pool = GamePool.Projectile.Pool;
            Pool.Spawn(Pool.Preset.PoolItems[2].Prefab);
        }
    }
}
