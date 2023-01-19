using Asteroids.Configs;
using Player.Stats;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFacade : BaseEnemyFacade
    {
        [Inject] private PlayerView _playerView;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private SaucerPool _pool;
        
        private float _speed;

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.SaucerFlySpeed;
        }

        public override void SetTrajectorySettings(ITrajectorySettings settings)
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
        
        public class SaucerPool : MemoryPool<ITrajectorySettings, SaucerFacade>
        {
            protected override void OnCreated(SaucerFacade facade)
            {
                facade.OnCreated();
            }
            
            protected override void OnSpawned(SaucerFacade facade)
            {
                facade.OnSpawned();
            }

            protected override void OnDespawned(SaucerFacade facade)
            {
                facade.OnDespawned();
            }
            
            protected override void Reinitialize(ITrajectorySettings settings, SaucerFacade facade)
            {
                facade.SetTrajectorySettings(settings);
            }
        }
    }
}