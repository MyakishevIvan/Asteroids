using System.Collections;
using System.Collections.Generic;
using Asteroids.Configs;
using Asteroids.Helper;
using Enemies.View;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Enemies
{
    public class EnemiesSpawner : IInitializable
    {
        [Inject] private AsteroidFactory _asteroidFactory;
        [Inject] private SaucerFactory _saucerFactory;
        [Inject] private AsteroidParticleFactory _asteroidParticleFactory;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private EnemiesSettingsContainer _enemiesSettingsContainer;
        
        private IEnemyFactory _enemyFactory;
        private BaseEnemyView _currentEnemyView;
        private List<BaseEnemyController> _spawnedEnemies;
        private Coroutine _enemiesSpawnRoutine;
        
        public void Initialize()
        {
            _spawnedEnemies = new List<BaseEnemyController>();
            _enemiesSettingsContainer.CreatCashedEnemiesSettings();
        }
        
        public void StartSpawnEnemies() => _enemiesSpawnRoutine = CoroutinesManager.StartRoutine(EnemiesSpawnRoutine()); 
        
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
            var enemy = enemyFactory.Creat(_enemiesSettingsContainer.GetTrajectorySettings());
            _spawnedEnemies.Add(enemy);
        }
        
        public void SpawnAsteroidParticle(Transform asteroidTransform)
        {
            var explodedAsteroidPosition = asteroidTransform.position;

            for (var i = 0; i < _balanceStorage.EnemiesConfig.EnemyParticlesCount; i++)
                CreatAsteroidParticle(explodedAsteroidPosition);
        }
        
        private void CreatAsteroidParticle(Vector3 explodedAsteroidPosition)
        {
            var settings = (_enemiesSettingsContainer.GetAsteroidTrajectorySettings());
            settings.SetExplodedPosition(explodedAsteroidPosition);
            var enemy = _asteroidParticleFactory.Creat(settings);
            _spawnedEnemies.Add(enemy);
        }
        
        public void RemoveDespawnedEnemyFromList(BaseEnemyController enemyController) => _spawnedEnemies.Remove(enemyController);

        public void StopSpawnAndDespawnEnemies()
        {
            CoroutinesManager.StopRoutine(_enemiesSpawnRoutine);
            
            foreach (var enemy in _spawnedEnemies.ToArray())
                enemy.DespawnEnemy();
        }

        public IEnumerator<BaseEnemyController> GetEnumerator() => _spawnedEnemies.GetEnumerator();
    }
}
