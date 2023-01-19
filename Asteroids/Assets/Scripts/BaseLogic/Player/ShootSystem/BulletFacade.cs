using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Signals;
using Zenject;

namespace Player.ShootSystem
{
    public class BulletFacade : BaseWeaponFacade
    {
        [Inject] private BulletPool _pool;
        
        public override void DespawnWeapon()
        {
            if (gameObject.activeSelf)
                _pool.Despawn(this);
        }

        protected override void HandleDamage(BaseEnemyFacade enemyFacade)
        {
            CancelInvoke(nameof(DespawnWeapon));
            enemyFacade.BulletDamage();
            DespawnWeapon();
            _signalBus.Fire(new RemoveWeaponFromActiveList(this));
        }
        
        public class BulletPool : MemoryPool<WeaponTrajectorySettings, BulletFacade>
        {
            [Inject] private BalanceStorage _balanceStorage;

            protected override void OnCreated(BulletFacade facade)
            {
                facade.OnCreated();
            }

            protected override void OnSpawned(BulletFacade facade)
            {
                facade.OnSpawned();
            }

            protected override void OnDespawned(BulletFacade facade)
            {
                facade.OnDespawned();
            }

            protected override void Reinitialize(WeaponTrajectorySettings settings, BulletFacade facade)
            {
                facade.Init(settings, _balanceStorage.WeaponConfig.BulletShootSpeed);
            }
        }
    }
}