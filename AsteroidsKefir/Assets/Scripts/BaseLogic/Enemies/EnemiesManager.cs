using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Enemies
{
    public class EnemiesManager : ITickable
    {
        [Inject] private AsteroidFactory _asteroidFactory;
        [Inject] private SaucerFactory _saucerFactory;
        [Inject] private AsteroidParticleFactory _asteroidParticleFactory;

        private const int CASHED_SETTINGS_COUNT = 100;
        private const int CASHED_ASTEROID_PARTICLE_SETTINGS_COUNT = 30;
        private BalanceStorage _balanceStorage;
        private IEnemyFactory _enemyFactory;
        private SignalBus _signalBus;
        private BaseEnemyView _currentEnemyView;
        private List<BaseEnemyFacede> _spawnedEnemies;
        private EnemyTrajectorySettings[] _cashedTrajectorySettings;
        private AsteroidParticleTrajectorySettings[] _cashedAsteroidParticleTrajectorySettings;
        private Coroutine _enemiesSpawnRoutine;
        private int _cashedSettingsIndex;
        private int _cashedAsteroidParticleSettingsIndex;

        private int CashedSettingsIndex
        {
            get => _cashedSettingsIndex;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _cashedSettingsIndex = value == CASHED_SETTINGS_COUNT ? 0 : value;
            }
        }

        private int CashedAsteroidParticleSettingsIndex
        {
            get => _cashedAsteroidParticleSettingsIndex;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _cashedAsteroidParticleSettingsIndex = value == CASHED_ASTEROID_PARTICLE_SETTINGS_COUNT ? 0 : value;
            }
        }

        public EnemiesManager(SignalBus signalBus, BalanceStorage balanceStorage, AsteroidFactory asteroidFactory)
        {
            _asteroidFactory = asteroidFactory;
            _signalBus = signalBus;
            _balanceStorage = balanceStorage;
            CashedSettingsIndex = 0;
            _spawnedEnemies = new List<BaseEnemyFacede>();
            _signalBus.Subscribe<RemoveEnemyFromActiveList>(RemoveDespawnedEnemyFromList);
            _signalBus.Subscribe<AsteroidBlowSignal>(SpawnAsteroidParticle);
            CreatCashedEnemiesSettings();
        }

        private void RemoveDespawnedEnemyFromList(RemoveEnemyFromActiveList signal)
        {
            _spawnedEnemies.Remove(signal.EnemyFacede);
        }

        private void SpawnAsteroidParticle(AsteroidBlowSignal signal)
        {
            var explodedAsteroidPosition = signal.AsroidTransform.position;

            for (var i = 0; i < _balanceStorage.EnemiesConfig.EnemyParticlesCount; i++)
                CreatAsteroidParticle(explodedAsteroidPosition);
        }

        private void CreatAsteroidParticle(Vector3 explodedAsteroidPosition)
        {
            var settings = _cashedAsteroidParticleTrajectorySettings[CashedAsteroidParticleSettingsIndex++];
            settings.SetExplodedPosition(explodedAsteroidPosition);
            var enemy = _asteroidParticleFactory.Creat(settings);
            _spawnedEnemies.Add(enemy);
        }

        private void CreatCashedEnemiesSettings()
        {
            CreatSettingsForIntegerEnemies();
            CreatSettingsForParticleEnemies();
        }

        private void CreatSettingsForIntegerEnemies()
        {
            var spawnRadius = _balanceStorage.EnemiesConfig.EnemySpawnRadius;
            var trajectoryVariance = _balanceStorage.EnemiesConfig.TrajectoryVariance;
            _cashedTrajectorySettings = new EnemyTrajectorySettings[CASHED_SETTINGS_COUNT];

            for (var i = 0; i < CASHED_SETTINGS_COUNT; i++)
                _cashedTrajectorySettings[i] = new EnemyTrajectorySettings(spawnRadius, trajectoryVariance);
        }

        private void CreatSettingsForParticleEnemies()
        {
            _cashedAsteroidParticleTrajectorySettings =
                new AsteroidParticleTrajectorySettings[CASHED_ASTEROID_PARTICLE_SETTINGS_COUNT];

            for (var i = 0; i < CASHED_ASTEROID_PARTICLE_SETTINGS_COUNT; i++)
                _cashedAsteroidParticleTrajectorySettings[i] = new AsteroidParticleTrajectorySettings();
        }

        public void StartSpawnEnemies()
        {
            _enemiesSpawnRoutine = CoroutinesManager.StartRoutine(EnemiesSpawnRoutine());
        }

        private IEnumerator EnemiesSpawnRoutine()
        {
            while (true)
            {
                SpawnEnemies();
                yield return new WaitForSeconds(_balanceStorage.EnemiesConfig.EnemySpawnDelay);
            }
        }

        private void SpawnEnemies()
        {
            for (var i = 0; i < _balanceStorage.EnemiesConfig.EnemySpawnCount; i++)
            {
                _enemyFactory = Random.value <= _balanceStorage.EnemiesConfig.SaucerSpawnChance
                    ? _saucerFactory
                    : _asteroidFactory;

                CreatEnemy(_enemyFactory);
            }
        }

        private void CreatEnemy(IEnemyFactory enemyFactory)
        {
            var enemy = enemyFactory.Creat(_cashedTrajectorySettings[CashedSettingsIndex++]);
            _spawnedEnemies.Add(enemy);
        }

        public void StopSpawnAndClearEnemies()
        {
            CoroutinesManager.StopRoutine(_enemiesSpawnRoutine);
            
            foreach (var enemy in _spawnedEnemies.ToArray())
                enemy.DespawnEnemy();
        }

        public void Tick()
        {
            foreach (var enemy in _spawnedEnemies)
                enemy.Move();
        }
    }
}