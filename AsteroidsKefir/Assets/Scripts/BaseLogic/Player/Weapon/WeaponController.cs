using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Helper;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Inject] private BalanceStorage _balanceStorage;
        private Vector3 _direction;
        private BaseWeapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<BaseWeapon>();
        }
        
        private void Update()
        {
            if (_direction == Vector3.zero)
                return;

            transform.position += _direction * (_balanceStorage.WeaponConfig.ShootSpeed * Time.deltaTime);
        }

        private void OnDisable()
        {
            _direction = Vector3.zero;
        }
        
        public void Init(Vector3 direction)
        {
            _direction = direction;
            Invoke(nameof(DestroyWeapon), 2);
        }

        private void DestroyWeapon()
        {
            if (gameObject.activeSelf)
                _weapon.Despawn();
        }
        
        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer(TextNameHelper.ENEMY))
            {
                _weapon.TakeDamage(()=> CancelInvoke(nameof(DestroyWeapon)));
                var enemyController = col.gameObject.GetComponent<BaseEnemyController>();
                enemyController.TakeDamage(_weapon);
            }
        }
    }
}