using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using Asteroids.Windows;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Player.ShootSystem
{
    public class PlayerShootSystem : IInitializable
    {
        [Inject] private PlayerHudParams _hudParams;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerInputAction _playerInputAction;
        [Inject] private BulletShootCreator _bulletShootCreator;
        [Inject] private RayShootCreator _rayShootCreator;
        private float _previouseTime;
        private float _currentTime;
        
        
        public void Initialize()
        {
            _playerInputAction.Player.ShootBullet.performed += ShootBulletOnperformed;
            _playerInputAction.Player.ShootRay.performed += ShootRayOnperformed;
            _previouseTime = _balanceStorage.WeaponConfig.ShootDelay;
        }

        private void ShootBulletOnperformed(InputAction.CallbackContext obj)
        {
            StartShooting(_bulletShootCreator);
        }

        private void ShootRayOnperformed(InputAction.CallbackContext obj)
        {
            StartShooting(_rayShootCreator);
        }
        
        public void UnsubscribeInput()
        {
            _playerInputAction.Player.ShootBullet.performed -= ShootBulletOnperformed;
            _playerInputAction.Player.ShootRay.performed -= ShootRayOnperformed;
        }

        private void StartShooting(ShootingCreator shootingCreator)
        {
            _currentTime = Time.time - _previouseTime;

            if (_currentTime < _balanceStorage.WeaponConfig.ShootDelay)
                return;
            
            _previouseTime = Time.time;
            shootingCreator.Shoot();
        }
    }
}