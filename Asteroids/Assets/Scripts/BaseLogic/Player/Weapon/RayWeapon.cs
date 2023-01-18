using System;
using Zenject;

namespace Asteroids.Player.Weapon
{
    public class RayWeapon : BaseWeapon
    {
        [Inject] private RayPool _rayPool;
        
        public override void Despawn()
        {
            _rayPool.Despawn(this);
        }

        public override void TakeDamage(Action onDamage)
        {
            
        }

        public class RayPool : MemoryPool<PlayerView, BaseWeapon>
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