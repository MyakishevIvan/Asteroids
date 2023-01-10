using System;
using Asteroids.Configs;
using Asteroids.Windows;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


namespace Asteroids.Player.ShootSystem
{
    public class PlayerShootSystem : IInitializable, IDisposable
    {
        [Inject] private PlayerHudParamsCounter _hudParamsCounter;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerInputAction _playerInputAction;
        [Inject] private BulletShootCreator _bulletShootCreator;
        [Inject] private RayShootCreator _rayShootCreator;
        private float _previousTime;
        private float _currentTime;
        
        
        public void Initialize()
        {
            _playerInputAction.Player.ShootBullet.performed += ShootBulletOnperformed;
            _playerInputAction.Player.ShootRay.performed += ShootRayOnperformed;
            _previousTime = _balanceStorage.WeaponConfig.ShootDelay;
        }

        private void ShootBulletOnperformed(InputAction.CallbackContext obj)
        {
            StartShooting(_bulletShootCreator);
        }

        private void ShootRayOnperformed(InputAction.CallbackContext obj)
        {
            StartShooting(_rayShootCreator);
        }
        
        private void StartShooting(ShootingCreator shootingCreator)
        {
            _currentTime = Time.time - _previousTime;

            if (_currentTime < _balanceStorage.WeaponConfig.ShootDelay)
                return;
            
            _previousTime = Time.time;
            shootingCreator.Shoot();
        }

        public void Dispose()
        {
            _playerInputAction?.Dispose();
        }
    }
}