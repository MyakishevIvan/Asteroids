using Asteroids.Configs;
using Asteroids.Enemies;
using Zenject;

namespace Player.ShootSystem
{
    public class RayFacade : BaseWeaponFacade
    {
        [Inject] private RayPool _pool;
        
        public override void DespawnWeapon()
        {
            if (gameObject.activeSelf)
                _pool.Despawn(this);
        }

        protected override void HandleDamage(BaseEnemyFacade enemyFacade)
        {
            enemyFacade.RayDamage();
        }
        
        public class RayPool : MemoryPool<WeaponTrajectorySettings, RayFacade>
        {
            [Inject] private BalanceStorage _balanceStorage;

            protected override void OnCreated(RayFacade facade)
            {
                facade.OnCreated();
            }

            protected override void OnSpawned(RayFacade facade)
            {
                facade.OnSpawned();
            }

            protected override void OnDespawned(RayFacade facade)
            {
                facade.OnDespawned();
            }

            protected override void Reinitialize(WeaponTrajectorySettings settings, RayFacade facade)
            {
                facade.Init(settings, _balanceStorage.WeaponConfig.RayShootSpeed);
            }
        }
    }
}