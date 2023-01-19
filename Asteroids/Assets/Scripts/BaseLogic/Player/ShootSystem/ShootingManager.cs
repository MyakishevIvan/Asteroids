using System;
using System.Collections.Generic;
using Asteroids.Configs;
using Asteroids.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.ShootSystem
{
    public class ShootingManager : IInitializable, IDisposable, ITickable
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerInputAction _playerInputAction;
        [Inject] private RayShootHandler _rayShootHandler;
        [Inject] private BulletShootHandler _bulletShootHandler;
        [Inject] private SignalBus _signalBus;
        
        private List<BaseWeaponFacade> _shotWeapons;
        private float _previousTime;
        private float _currentTime;

        public void SubscribeShootEvents()
        {
            _playerInputAction.Player.ShootBullet.performed += ShootBulletOnPerformed;
            _playerInputAction.Player.ShootRay.performed += ShootRayOnPerformed;
            _currentTime = 0;
            _previousTime = 0;
        }

        public void UnSubscribeShootEvents()
        {
            _playerInputAction.Player.ShootBullet.performed -= ShootBulletOnPerformed;
            _playerInputAction.Player.ShootRay.performed -= ShootRayOnPerformed;
        }

        public void Initialize()
        {
            _shotWeapons = new List<BaseWeaponFacade>();
            _signalBus.Subscribe<RemoveWeaponFromActiveList>(RemoveWeaponFromActiveList);
        }

        private void RemoveWeaponFromActiveList(RemoveWeaponFromActiveList signal) =>
            _shotWeapons.Remove(signal.WeaponFacade);
        

            private void ShootBulletOnPerformed(InputAction.CallbackContext obj)
        {
            TryShooting(_bulletShootHandler);
        }

        private void ShootRayOnPerformed(InputAction.CallbackContext obj)
        {
            TryShooting(_rayShootHandler);
        }

        private void TryShooting(BaseShootHandler shootHandler)
        {
            _currentTime = Time.time - _previousTime;

            if (_currentTime < _balanceStorage.WeaponConfig.ShootDelay)
                return;

            if (shootHandler.TryShoot(out var weaponFacade))
            {
                _shotWeapons.Add(weaponFacade);
                _previousTime = Time.time;
            }
        }

        public void DespawnAllShotWeapon()
        {
            foreach (var weaponFacade in _shotWeapons.ToArray())
                weaponFacade.DespawnWeapon();
            
            _shotWeapons.Clear();
        }

        public void Dispose()
        {
            _playerInputAction?.Dispose();
        }

        public void Tick()
        {
            foreach (var weapon in _shotWeapons)
                weapon.Move();
        }
    }
}