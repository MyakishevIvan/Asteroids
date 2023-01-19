using System.Collections;
using Asteroids.Helper;
using Player.Stats;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Player.ShootSystem
{
    public class RayShootHandler : BaseShootHandler
    {
        [Inject] private PlayerStatsManager _playerStatsManager;
        [Inject] private SignalBus _signalBus;
        [Inject] private RayFacade.RayPool _pool;
        private Coroutine _reloadRoutine;
        
        public override bool TryShoot(out BaseWeaponFacade weaponFacade)
        {
            weaponFacade = null;

            if (!_playerStatsManager.TrySpendRays())
                return false;
            
            _trajectorySettings.Init(_playerView);
            weaponFacade = _pool.Spawn(_trajectorySettings);
            return true;
        }
        
    }
}