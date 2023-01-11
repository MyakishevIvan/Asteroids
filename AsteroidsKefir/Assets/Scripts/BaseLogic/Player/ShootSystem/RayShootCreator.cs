using System.Collections;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.ShootSystem
{
    public class RayShootCreator : ShootingCreator
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerStatsStorage _playerStatsStorage;
        [Inject] private SignalBus _signalBus;
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
            if (_playerStatsStorage.RayReloadTime != 0)
                return false;

            _playerStatsStorage.RayCount--;

            if (_playerStatsStorage.RayCount == 0)
            {
                _signalBus.Fire<RayEndedSignal>();
                _reloadRoutine = CoroutinesManager.StartRoutine(SpendReloadTime());
            }
            return true;
        }

        private IEnumerator SpendReloadTime()
        {
            while (_playerStatsStorage.RayReloadTime != 0)
            {
                yield return new WaitForSeconds(1);
                _playerStatsStorage.RayReloadTime--;
            }

            _signalBus.Fire<RayReloadTimeEned>();
            CoroutinesManager.StopRoutine(_reloadRoutine);
        }
        
    }
}