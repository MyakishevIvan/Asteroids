using System;
using Asteroids.Configs;
using Asteroids.Player;
using Asteroids.Player.Weapon;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFacade : BaseEnemyFacede
    {
        [Inject] private PlayerView _playerView;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private SaucerPool _pool;
        [Inject] private SignalBus _signalBus;
        
        private float _speed;

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.SaucerFlySpeed;
        }

        public override void SetTrajectorySettings(IEnemyTrajectorySettings settings)
        {
            transform.position = settings.SpawnPoint;
        }

        public override void BulletDamage()
        {
            HitEnemy();
        }

        public override void RayDamage()
        {
            HitEnemy();
        }

        protected override void HitEnemy()
        {
            DespawnEnemy();
        }

        public override void DespawnEnemy() => _pool.Despawn(this);
        
        public override void Move()
        {
            if (_playerView != null)
                transform.position =
                    Vector3.MoveTowards(transform.position, _playerView.transform.position, _speed * Time.deltaTime);
        }
        
        public class SaucerPool : MemoryPool<IEnemyTrajectorySettings, SaucerFacade>
        {
            [Inject] private DiContainer _diContainer;
            [Inject] private SignalBus _signalBus;
            protected override void OnCreated(SaucerFacade facade)
            {
                facade.gameObject.SetActive(false);
            }
            
            protected override void OnSpawned(SaucerFacade facade)
            {
                facade.gameObject.SetActive(true);
            }

            protected override void OnDespawned(SaucerFacade facade)
            {
                _signalBus.Fire(new RemoveEnemyFromActiveList(facade));
                facade.gameObject.SetActive(false);
            }
            
            protected override void Reinitialize(IEnemyTrajectorySettings settings, SaucerFacade facade)
            {
                facade.SetTrajectorySettings(settings);
            }
        }
    }
}