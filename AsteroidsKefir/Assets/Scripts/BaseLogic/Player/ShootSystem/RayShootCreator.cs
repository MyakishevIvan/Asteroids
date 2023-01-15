using System.Collections;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Helper;
using Asteroids.Player.Weapon;
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
        [Inject] private RayWeapon.RayPool _pool;
        private Coroutine _reloadRoutine;
        private BaseWeapon _currentRay;

        public override void CreatWeapon()
        {
            if(!CanStartRayRayShooting())
                return;
            
            _currentRay = _pool.Spawn(_playerView);
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