using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Signals;
using Zenject;

namespace Player.ShootSystem
{
    public class BulletController : BaseWeaponController
    {
        [Inject] private BulletPool _pool;
        
        public override void DespawnWeapon()
        {
            if (gameObject.activeSelf)
                _pool.Despawn(this);
        }

        protected override void HandleDamage(BaseEnemyController enemyController)
        {
            enemyController.BulletDamage();
            DespawnWeapon();
            _signalBus.Fire(new RemoveWeaponFromActiveListSignal(this));
        }
        
        public override void OnSpawned()
        {
            _soundsController.PlaySound(SoundType.BulletSound);
            base.OnSpawned();
        }
        
        public class BulletPool : MemoryPool<WeaponTrajectorySettings, BulletController>
        {
            [Inject] private BalanceStorage _balanceStorage;

            protected override void OnCreated(BulletController controller)
            {
                controller.OnCreated();
            }

            protected override void OnSpawned(BulletController controller)
            {
                controller.OnSpawned();
            }

            protected override void OnDespawned(BulletController controller)
            {
                controller.OnDespawned();
            }

            protected override void Reinitialize(WeaponTrajectorySettings settings, BulletController controller)
            {
                controller.Init(settings, _balanceStorage.WeaponConfig.BulletShootSpeed);
            }
        }
    }
}