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
    public class EnemiesManager : ITickable, IInitializable
    {
        [Inject] private AsteroidFactory _asteroidFactory;
        [Inject] private SaucerFactory _saucerFactory;
        
        private BalanceStorage _balanceStorage;
        private IEnemyFactory _enemyFactory;
        private SignalBus _signalBus;
        private BaseEnemy _currentEnemy;
        private List<BaseEnemy> _spawnedEnemies;
        private EnemyTrajectorySettings[] _cashedTrajectorySettings;
        private const int CASHED_SETTINGS_COUNT = 100;
        private Coroutine _enemiesSpawnRoutine;

        private int _cashedSettingsIndex;

        private int CashedSettingsIndex
        {
            get => _cashedSettingsIndex;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _cashedSettingsIndex = value == CASHED_SETTINGS_COUNT ? 0 : value;
            }
        }

        public void Initialize()
        {
            // CashedSettingsIndex = 0;
            // _spawnedEnemies = new List<BaseEnemy>();
            // _signalBus.Subscribe<RemoveEnemyFromActiveList>(RemoveDespawnedEnemyFromList);
            // var spawnRadius = _balanceStorage.EnemiesConfig.EnemySpawnRadius;
            // var trajectoryVariance = _balanceStorage.EnemiesConfig.TrajectoryVariance;
            // CreatCashedEnemiesSettings(spawnRadius, trajectoryVariance);
        }

        public EnemiesManager(SignalBus signalBus, BalanceStorage balanceStorage, AsteroidFactory asteroidFactory)
        {
            _asteroidFactory = asteroidFactory;
            _signalBus = signalBus;
            _balanceStorage = balanceStorage;
            CashedSettingsIndex = 0;
            _spawnedEnemies = new List<BaseEnemy>();
            _signalBus.Subscribe<RemoveEnemyFromActiveList>(RemoveDespawnedEnemyFromList);
            var spawnRadius = _balanceStorage.EnemiesConfig.EnemySpawnRadius;
            var trajectoryVariance = _balanceStorage.EnemiesConfig.TrajectoryVariance;
            CreatCashedEnemiesSettings(spawnRadius, trajectoryVariance);
        }

        private void CreatCashedEnemiesSettings(int spawnRadius, int trajectoryVariance)
        {
            _cashedTrajectorySettings = new EnemyTrajectorySettings[CASHED_SETTINGS_COUNT];

            for (var i = 0; i < CASHED_SETTINGS_COUNT; i++)
                _cashedTrajectorySettings[i] = new EnemyTrajectorySettings(spawnRadius, trajectoryVariance);
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
            _currentEnemy = enemyFactory.Creat(_cashedTrajectorySettings[CashedSettingsIndex]);
            _spawnedEnemies.Add(_currentEnemy);
            CashedSettingsIndex++;
        }

        public void StopSpawnAndClearEnemies()
        {
            CoroutinesManager.StopRoutine(_enemiesSpawnRoutine);
            _spawnedEnemies.Clear();
        }

        public void Tick()
        {
            foreach (var enemy in _spawnedEnemies)
                enemy.Move();
        }

        private void RemoveDespawnedEnemyFromList(RemoveEnemyFromActiveList signal) =>
            _spawnedEnemies.Remove(signal.Enemy);

    }

    public class EnemyTrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector2 SpawnDistance { get; set; }

        public EnemyTrajectorySettings(int enemySpawnRadius, int trajectoryVariance)
        {
            SpawnDistance = Random.insideUnitCircle.normalized;
            SpawnPoint = SpawnDistance * enemySpawnRadius;
            var variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        }
    }
}