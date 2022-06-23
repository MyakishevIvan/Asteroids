using System;
using System.Collections;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Player.Weapon;
using Asteroids.Windows;
using UnityEngine;

namespace Asteroids.Player.ShootSystem
{
    public class RayShootCreator : ShootingCreator
    {
        private WeaponConfig _weaponConfig;
        private PlayerHudParams _playerHudParams;
        private Coroutine _reloadRoutine;
        
        public RayShootCreator(WeaponView weapon, Action<WeaponView> OnShoot, WeaponConfig weaponConfig, PlayerHudParams playerHudParams) : base(weapon, OnShoot)
        {
            _weaponConfig = weaponConfig;
            _playerHudParams = playerHudParams;
            _playerHudParams.rayCount = _weaponConfig.RayShootCount;
        }

        public override void Shoot()
        {
            if(!CanStartRayRayShooting())
                return;
            
            base.Shoot();
        }
        
        private bool CanStartRayRayShooting()
        {
            if (_playerHudParams.rayReloadTime != 0)
                return false;

            _playerHudParams.rayCount--;

            if (_playerHudParams.rayCount == 0)
            {
                _playerHudParams.rayReloadTime = _weaponConfig.ReloadTime;
                _reloadRoutine = CoroutinesManager.StartRoutine(SpendReloadTime());
            }
            return true;
        }

        private IEnumerator SpendReloadTime()
        {
            while (_playerHudParams.rayReloadTime != 0)
            {
                yield return new WaitForSeconds(1);
                _playerHudParams.rayReloadTime--;
            }

            _playerHudParams.rayCount = _weaponConfig.RayShootCount;
            CoroutinesManager.StopRoutine(_reloadRoutine);
        }
    }
}