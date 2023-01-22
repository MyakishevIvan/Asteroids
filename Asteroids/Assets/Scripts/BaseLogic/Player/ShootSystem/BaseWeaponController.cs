using System;
using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Helper;
using UnityEngine;
using Zenject;

namespace Player.ShootSystem
{
    public abstract class BaseWeaponController : MonoBehaviour, IObjectPoolable
    {
        [Inject] protected SignalBus _signalBus;
        [Inject] private BalanceStorage _balanceStorage;
        private int _weaponSpeed;
        private Vector3 _direction;

        protected void Init(WeaponTrajectorySettings settings, int weaponSpeed)
        {
            SetSettings(settings, weaponSpeed);
            Invoke(nameof(DespawnWeapon), _balanceStorage.WeaponConfig.WeaponLifeTime);
        }

        private void SetSettings(WeaponTrajectorySettings settings, int weaponSpeed)
        {
            if (settings.Direction == Vector2.zero)
                throw new Exception("Weapon no direction");

            if (weaponSpeed <= 0)
                throw new Exception("Incorrect speed for weapon");

            transform.position = settings.SpawnPoint;
            transform.rotation = settings.Rotation;
            _direction = settings.Direction;
            _weaponSpeed = weaponSpeed;
        }
        
        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            CancelInvoke(nameof(DespawnWeapon));
            gameObject.SetActive(false);
        }

        public void OnCreated()
        {
            gameObject.SetActive(false);
        }
        
        public void Move() => transform.position += _direction * (_weaponSpeed * Time.deltaTime);
        private void OnDisable() => _direction = Vector3.zero;

        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer(TextNameHelper.ENEMY))
            {
                var enemyController = col.gameObject.GetComponent<BaseEnemyController>();
                HandleDamage(enemyController);
            }
        }

        public abstract void DespawnWeapon();
        protected abstract void HandleDamage(BaseEnemyController enemyController);
    }
}