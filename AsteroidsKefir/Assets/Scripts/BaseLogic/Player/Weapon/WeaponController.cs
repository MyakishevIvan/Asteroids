using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Player.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponConfig _weaponConfig;
        private Vector3 _direction;
        private WeaponType _weaponType;

        private void Awake()
        {
            _weaponType = GetComponent<WeaponView>().WeaponType;
            Invoke(nameof(DestroyWeapon), 5f);
            _weaponConfig = BalanceStorage.instance.WeaponConfig;
        }

        private void DestroyWeapon() => Destroy(gameObject);

        private void Update()
        {
            if (_direction == Vector3.zero)
                return;

            transform.position += _direction * (_weaponConfig.ShootSpeed * Time.deltaTime);
        }

        public void AddDirection(Vector3 direction)
        {
            _direction = direction;
        }

        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (_weaponType == WeaponType.Bullet)
                    Destroy(gameObject);
                
                var enemyController = col.gameObject.GetComponent<EnemyController>();
                enemyController.DamageEnemy(_weaponType);
            }
        }
    }
}