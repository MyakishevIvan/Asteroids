using System;
using Asteroids.Enemies;
using Asteroids.Enums;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.Weapon
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        protected static void SetWeapon(PlayerView player, BaseWeapon baseWeapon)
        {
            baseWeapon.transform.position = player.transform.position;
            baseWeapon.transform.rotation = Quaternion.Euler(0, 0, player.transform.rotation.eulerAngles.z);
            baseWeapon.GetComponent<WeaponController>().Init(player.transform.up);
        }

        public abstract void Despawn();
        public abstract void TakeDamage(Action onDamage);

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