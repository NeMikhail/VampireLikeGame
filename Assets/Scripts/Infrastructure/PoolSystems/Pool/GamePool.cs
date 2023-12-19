using UnityEngine;
using Core;
using Core.Interface;
using Core.ResourceLoader;
using MVP.Views.CollectableViews;
using MVP.Views.EnemyViews;
using MVP.Views.WeaponViews;
using MVP.Views.ExperienceViews;
using UI;

namespace Infrastructure.PoolSystems.Pool
{
    internal static class GamePool
    {
        private static GamePoolBlock<ProjectileView> s_projectile;
        private static GamePoolBlock<EnemyView> s_enemy;
        private static GamePoolBlock<EnemyBulletView> s_enemybullet;
        private static GamePoolBlock<CollectableView> s_collectable;
        private static GamePoolBlock<ExperienceView> s_experience;
        private static GamePoolBlock<DamagePopupView> s_damagePopup;

        public static GamePoolBlock<ProjectileView> Projectile
        {
            get => s_projectile;
            set => s_projectile = value;
        }
        internal static GamePoolBlock<EnemyView> Enemy
        {
            get => s_enemy;
            set => s_enemy = value;
        }
        internal static GamePoolBlock<EnemyBulletView> EnemyBullet
        {
            get => s_enemybullet;
            set => s_enemybullet = value;
        }
        internal static GamePoolBlock<CollectableView> Collectable
        {
            get => s_collectable;
            set => s_collectable = value;
        }
        internal static GamePoolBlock<ExperienceView> Experience
        {
            get => s_experience;
            set => s_experience = value;
        }
        internal static GamePoolBlock<DamagePopupView> DamagePopup
        {
            get => s_damagePopup;
            set => s_damagePopup = value;
        }

        public sealed class GamePoolBlock<T> where T : MonoBehaviour, IPoolObject<T>
        {
            private Pool<T> _pool;

            public Pool<T> Pool
            {
                get => _pool;
                set => _pool = value;
            }

            public GamePoolBlock(string path)
            {
                GamePoolPreset element = ResourceLoadManager.GetConfig<GamePoolPreset>(path);
                _pool = new Pool<T>(element);
            }
        }


        public static void Init()
        {
            s_projectile = new GamePoolBlock<ProjectileView>(ConstantsProvider.NAME_PROJECTILE_POOL_PRESET);
            s_enemy = new GamePoolBlock<EnemyView>(ConstantsProvider.NAME_ENEMY_POOL_PRESET);
            s_enemybullet = new GamePoolBlock<EnemyBulletView>(ConstantsProvider.NAME_ENEMY_BULLET_POOL_PRESET);
            s_collectable = new GamePoolBlock<CollectableView>(ConstantsProvider.NAME_COLLECTABLE_POOL_PRESET);
            s_experience = new GamePoolBlock<ExperienceView>(ConstantsProvider.EXPERIENCE_CONFIG_NAME);
            s_damagePopup = new GamePoolBlock<DamagePopupView>(ConstantsProvider.NAME_DAMAGE_POPUP_POOL_PRESET);
        }

        public static void Clear()
        {
            s_projectile.Pool.ClearPool();
            s_enemy.Pool.ClearPool();
            s_collectable.Pool.ClearPool();
            s_experience.Pool.ClearPool();
            s_damagePopup.Pool.ClearPool();
        }
    }
}