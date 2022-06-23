using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using Asteroids.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Player.ShootSystem
{
    public class PlayerShootSystem
    {
        private Transform _playerTransform;
        private WeaponConfig _weaponConfig;
        private GameObject _weaponContainer;
        private Coroutine _reloadRoutine;
        private PlayerHudParams _hudParams;
        private float _currentShootDelay;
        private BulletShootCreator _bulletShootCreator;
        private RayShootCreator _rayShootCreator;
        private ShootingCreator _shootingCreator;

        public PlayerShootSystem(Transform playerTransform)
        {
            _hudParams = PlayerHudParams.Instance;
            _weaponConfig = BalanceStorage.instance.WeaponConfig;
            _hudParams.rayReloadTime = 0;
            _playerTransform = playerTransform;
            _bulletShootCreator = new BulletShootCreator(_weaponConfig.GetWeaponView(WeaponType.Bullet), Shoot);
            _rayShootCreator = new RayShootCreator(_weaponConfig.GetWeaponView(WeaponType.Ray), Shoot, _weaponConfig, _hudParams);
            
            _weaponContainer = new GameObject("WeaponContainer");
        }
        
        public void ShootUpdate()
        {
            _currentShootDelay += Time.deltaTime;
            
            if (_currentShootDelay < _weaponConfig.ShootDelay)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                StartShooting(_bulletShootCreator);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                StartShooting(_rayShootCreator);
            }
        }

        private void StartShooting(ShootingCreator shootingCreator)
        {
            _currentShootDelay = 0;
            shootingCreator.Shoot();
        }

        private void Shoot(WeaponView weapon)
        {
            var weaponObj = Object.Instantiate
            (weapon, _playerTransform.position, Quaternion.Euler(0, 0, _playerTransform.rotation.eulerAngles.z),
                _weaponContainer.transform);
            
           var weaponController = weaponObj.gameObject.AddComponent<WeaponController>();
           weaponController.AddDirection(_playerTransform.transform.up);
        }
    }
}