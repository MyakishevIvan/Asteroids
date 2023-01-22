using Asteroids.Configs;
using Player.Stats;
using Player.View;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerController : BaseEnemyController
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
        
        public class SaucerPool : MemoryPool<ITrajectorySettings, SaucerController>
        {
            protected override void OnCreated(SaucerController controller)
            {
                controller.OnCreated();
            }
            
            protected override void OnSpawned(SaucerController controller)
            {
                controller.OnSpawned();
            }

            protected override void OnDespawned(SaucerController controller)
            {
                controller.OnDespawned();
            }
            
            protected override void Reinitialize(ITrajectorySettings settings, SaucerController controller)
            {
                controller.SetTrajectorySettings(settings);
            }
        }
    }
}