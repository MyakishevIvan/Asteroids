using System.Collections;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidFacade : BaseEnemyFacade, ITemporaryEnemy
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private AsteroidPool _pool;
        [Inject] private BalanceStorage _balanceStorage;
        
        private TrajectorySettings _settings;
        private Vector2 _currentDirection;
        private float _speed;
        
        public Coroutine DespawnRoutine { get; set; }

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.AsteroidFlySpeed;
        }

        public override void SetTrajectorySettings(ITrajectorySettings settings)
        {
            _settings = (TrajectorySettings)settings;
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
            _signalBus.Fire(new AsteroidBlowSignal(EnemyView.transform));
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
        
        public void StartDespawnAfterLifeTimeRoutine() =>
            DespawnRoutine = CoroutinesManager.StartRoutine(DespawnCoroutine());
        
        public IEnumerator DespawnCoroutine()
        {
            yield return new WaitForSeconds(_balanceStorage.EnemiesConfig.AsteroidLifeTime);
            DespawnEnemy();
        }

        public override void DespawnEnemy() => _pool.Despawn(this);
        
        public void StopDespawnAfterLifeTimeRoutine() => CoroutinesManager.StopRoutine(DespawnRoutine);
        
        public class AsteroidPool : MemoryPool<ITrajectorySettings, AsteroidFacade>
        {
            [Inject] private SignalBus _signalBus;

            // Called immediately after the item is first added to the pool
            protected override void OnCreated(AsteroidFacade facade)
            {
                facade.gameObject.SetActive(false);
            }

            // Called immediately after the item is removed from the pool
            protected override void OnSpawned(AsteroidFacade facade)
            {
                facade.gameObject.SetActive(true);
            }

            // Called immediately after the item is returned to the pool
            protected override void OnDespawned(AsteroidFacade facade)
            {
                _signalBus.Fire(new RemoveEnemyFromActiveList(facade));
                facade.gameObject.SetActive(false);
            }

            // Similar to OnSpawned
            // Called immediately after the item is removed from the pool
            // This method will also contain any parameters that are passed along
            // to the memory pool from the spawning code
            protected override void Reinitialize(ITrajectorySettings settings, AsteroidFacade enemy)
            {
                enemy.SetTrajectorySettings(settings);
                enemy.StartDespawnAfterLifeTimeRoutine();
            }
        }
    }
}