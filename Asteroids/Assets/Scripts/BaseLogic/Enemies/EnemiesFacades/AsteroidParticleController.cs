using System.Collections;
using Asteroids.Configs;
using Asteroids.Helper;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleController : BaseEnemyController, ITemporaryEnemy
    {
        [Inject] private AsteroidParticlePool _pool;
        [Inject] private BalanceStorage _balanceStorage;
        
        private AsteroidParticleTrajectorySettings _settings;
        private Vector2 _currentDirection;
        private int _speed;

        public Coroutine DespawnRoutine { get; set; }

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.AsteroidParticleFlySpeed;
        }
        
        public override void SetTrajectorySettings(ITrajectorySettings settings)
        {
            _settings = (AsteroidParticleTrajectorySettings)settings;
            transform.position = _settings.SpawnPoint;
        }

        public override void Move()
        {
            _currentDirection = _settings.Direction * (_speed * Time.deltaTime);
            transform.position = new Vector2
                (transform.position.x + _currentDirection.x, transform.position.y + _currentDirection.y);
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
            _pool.Despawn(this);
        }

        public IEnumerator DespawnCoroutine()
        {
            yield return new WaitForSeconds(_balanceStorage.EnemiesConfig.AsteroidLifeTime);
            DespawnEnemy();
        }

        public override void DespawnEnemy() => _pool.Despawn(this);

        protected override void OnDespawned()
        {
            StopDespawnAfterLifeTimeRoutine();
            base.OnDespawned();
        }
        
        public void StopDespawnAfterLifeTimeRoutine() => CoroutinesManager.StopRoutine(DespawnRoutine);
        
        public void StartDespawnAfterEndLifeTimeRoutine() =>
            DespawnRoutine = CoroutinesManager.StartRoutine(DespawnCoroutine());
        
        public class AsteroidParticlePool : MemoryPool<ITrajectorySettings, AsteroidParticleController>
        {
            [Inject] private SignalBus _signalBus;
            private AsteroidParticleController _asteroidParticleController;

            protected override void OnCreated(AsteroidParticleController controller)
            {
                controller.OnCreated();
            }

            protected override void OnSpawned(AsteroidParticleController controller)
            {
                controller.OnSpawned();
            }

            protected override void OnDespawned(AsteroidParticleController controller)
            {
               controller.OnDespawned();
            }

            protected override void Reinitialize(ITrajectorySettings settings, AsteroidParticleController controller)
            {
                controller.SetTrajectorySettings(settings);
                controller.StartDespawnAfterEndLifeTimeRoutine();
            }
        }
    }
}