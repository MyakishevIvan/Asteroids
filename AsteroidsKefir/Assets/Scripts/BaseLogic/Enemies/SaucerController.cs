using System;
using Asteroids.Configs;
using Asteroids.Player;
using Asteroids.Player.Weapon;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerController : BaseEnemyController
    {
        [Inject] private PlayerView _playerView;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private Saucer.SaucerPool _pool;
        [Inject] private SignalBus _signalBus;
        
        private float _speed;

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.SaucerFlySpeed;
        }

        public override void SetTrajectorySettings(EnemyTrajectorySettings settings)
        {
            transform.position = settings.SpawnPoint;
        }

        public override void TakeDamage(BaseWeapon weapon)
        {
            _signalBus.Fire(new RemoveEnemyFromActiveList(_enemy));
            _pool.Despawn(GetComponent<Saucer>());
        }

        public override void Move()
        {
            if (_playerView != null)
                transform.position =
                    Vector3.MoveTowards(transform.position, _playerView.transform.position, _speed * Time.deltaTime);
        }

      
    }
}