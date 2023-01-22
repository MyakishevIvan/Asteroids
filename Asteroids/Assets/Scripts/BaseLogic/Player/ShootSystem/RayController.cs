using Asteroids.Configs;
using Asteroids.Enemies;
using Zenject;

namespace Player.ShootSystem
{
    public class RayController : BaseWeaponController
    {
        [Inject] private RayPool _pool;
        
        public override void DespawnWeapon()
        {
            if (gameObject.activeSelf)
                _pool.Despawn(this);
        }

        protected override void HandleDamage(BaseEnemyController enemyController)
        {
            enemyController.RayDamage();
        }
        
        public class RayPool : MemoryPool<WeaponTrajectorySettings, RayController>
        {
            [Inject] private BalanceStorage _balanceStorage;

            protected override void OnCreated(RayController controller)
            {
                controller.OnCreated();
            }

            protected override void OnSpawned(RayController controller)
            {
                controller.OnSpawned();
            }

            protected override void OnDespawned(RayController controller)
            {
                controller.OnDespawned();
            }

            protected override void Reinitialize(WeaponTrajectorySettings settings, RayController controller)
            {
                controller.Init(settings, _balanceStorage.WeaponConfig.RayShootSpeed);
            }
        }
    }
}