using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using Asteroids.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Player.ShootSystem
{
    public class PlayerShootSystem : IInitializable
    {
        [Inject] private PlayerHudParams _hudParams;
        [Inject] private BalanceStorage _balanceStorage;
        private Coroutine _reloadRoutine;
        private float _currentShootDelay;
        [Inject] private BulletShootCreator _bulletShootCreator;
        [Inject] private RayShootCreator _rayShootCreator;
        private ShootingCreator _shootingCreator;
        
        public void Initialize()
        {
            _hudParams.rayReloadTime = 0;
        }
        
        public void ShootUpdate()
        {
            _currentShootDelay += Time.deltaTime;
            
            if (_currentShootDelay < _balanceStorage.WeaponConfig.ShootDelay)
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
    }
}