using System.Collections;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Helper;
using Asteroids.Windows;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.ShootSystem
{
    public class RayShootCreator : ShootingCreator
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerHudParams _playerHudParams;
        private Coroutine _reloadRoutine;
        
        protected override void SelectWeapon()
        {
            _weapon = _balanceStorage.WeaponConfig.GetWeaponView(WeaponType.Ray);
        }
        
        public RayShootCreator( ) : base("Ray")
        {
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
                _playerHudParams.rayReloadTime = _balanceStorage.WeaponConfig.ReloadTime;
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

            _playerHudParams.rayCount = _balanceStorage.WeaponConfig.RayShootCount;
            CoroutinesManager.StopRoutine(_reloadRoutine);
        }
        
    }
}