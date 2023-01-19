using System.Collections;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleFacade : BaseEnemyFacade, ITemporaryEnemy
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private AsteroidParticlePool _pool;
        [Inject] private BalanceStorage _balanceStorage;
        
        private AsteroidParticleTrajectorySettings _settings;
        private Vector2 _currentDirection;
        private int _speed;

        public Coroutine DespawnRoutine { get; set; }

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.AsteroidFlySpeed;
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
            CoroutinesManager.StopRoutine(DespawnRoutine);
        }

        public IEnumerator DespawnCoroutine()
        {
            yield return new WaitForSeconds(_balanceStorage.EnemiesConfig.AsteroidLifeTime);
            DespawnEnemy();
        }

        public override void DespawnEnemy() => _pool.Despawn(this);

        
        public void StartDespawnAfterLifeTimeRoutine() =>
            DespawnRoutine = CoroutinesManager.StartRoutine(DespawnCoroutine());
        
        public void StopDespawnAfterLifeTimeRoutine() => CoroutinesManager.StopRoutine(DespawnRoutine);
        
        public class AsteroidParticlePool : MemoryPool<ITrajectorySettings, AsteroidParticleFacade>
        {
            [Inject] private DiContainer _diContainer;
            [Inject] private SignalBus _signalBus;
            private AsteroidParticleFacade _asteroidParticleFacade;

            protected override void OnCreated(AsteroidParticleFacade facade)
            {
                facade.gameObject.SetActive(false);
            }

            protected override void OnSpawned(AsteroidParticleFacade facade)
            {
                facade.gameObject.SetActive(true);
            }

            protected override void OnDespawned(AsteroidParticleFacade facade)
            {
                _signalBus.Fire(new RemoveEnemyFromActiveList(facade));
                facade.gameObject.SetActive(false);
            }

            protected override void Reinitialize(ITrajectorySettings settings, AsteroidParticleFacade facade)
            {
                facade.SetTrajectorySettings(settings);
                facade.StartDespawnAfterLifeTimeRoutine();
            }
        }
    }
}