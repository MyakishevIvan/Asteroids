using System;
using Zenject;

namespace Asteroids.Player.Weapon
{
    public class BulletWeapon : BaseWeapon
    {
        [Inject] private BulletPool _bulletPool;

        public override void Despawn()
        {
            _bulletPool.Despawn(this);
        }

        public override void TakeDamage(Action onDamage)
        {
            _bulletPool.Despawn(this);
            onDamage?.Invoke();
        }

        public class BulletPool : MemoryPool<PlayerView, BaseWeapon>
        {
            [Inject] private DiContainer _diContainer;

            protected override void OnCreated(BaseWeapon baseWeapon)
            {
                baseWeapon.gameObject.SetActive(false);
                _diContainer.InstantiateComponent<WeaponController>(baseWeapon.gameObject);
            }

            protected override void OnSpawned(BaseWeapon baseWeapon)
            {
                baseWeapon.gameObject.SetActive(true);
            }

            protected override void OnDespawned(BaseWeapon baseWeapon)
            {
                baseWeapon.gameObject.SetActive(false);
            }

            protected override void Reinitialize(PlayerView player, BaseWeapon baseWeapon)
            {
                SetWeapon(player, baseWeapon);
            }

        }
    }
}