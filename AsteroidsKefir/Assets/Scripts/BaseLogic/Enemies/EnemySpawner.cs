using System.Collections;
using System.Collections.Generic;
using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Helper;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable
{
    [Inject] private BalanceStorage _balanceStorage;
    [Inject] private DiContainer _diContainer;
    private Transform _playerTransform;
    [Inject] private EnemiesControlSystem _enemiesControlSystem;
    private GameObject _enemiesContainer;

    
    public void Initialize()
    {
        _enemiesContainer = new GameObject("EnemyContainer");
        EnemiesControlSystem.OnAsteroidDamage += InstantiateAsteroidParticle;
        CoroutinesManager.StartRoutine(EnemySpawnRoutine());
    }
    
    public void UnsubscribeEvents()
    {
        EnemiesControlSystem.OnAsteroidDamage -= InstantiateAsteroidParticle;
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_balanceStorage.EnemiesConfig.EnemySpawnDelay);
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < _balanceStorage.EnemiesConfig.EnemySpawnCount; i++)
        {
            var enemyType = Random.value <= _balanceStorage.EnemiesConfig.SaucerSpawnChance
                ? EnemyType.Saucer
                : EnemyType.Asteroid;

            var enemyView =
                GetEnemyInstantiateParams(enemyType, out var spawnDirection, out var spawnPoint, out var rotation);
            InstantiateEnemy(enemyView, spawnPoint, rotation, spawnDirection, enemyType);
        }
    }

    private void InstantiateEnemy(EnemyView enemyView, Vector2 spawnPoint, Quaternion rotation, Vector2 spawnDirection,
        EnemyType enemyType)
    {
        var enemyObj =
            _diContainer.InstantiatePrefabForComponent<EnemyView>(enemyView, spawnPoint, rotation,
                _enemiesContainer.transform);
        var direction = rotation * -spawnDirection;
        var enemyController = _diContainer.InstantiateComponent<EnemyController>(enemyObj.gameObject);

        if (enemyType == EnemyType.Asteroid || enemyType == EnemyType.AsteroidParticle)
            enemyController.SetDirection(direction);
    }

    private EnemyView GetEnemyInstantiateParams(EnemyType enemyType, out Vector2 spawnDirection, out Vector2 spawnPoint,
        out Quaternion rotation)
    {
        var enemyView = _balanceStorage.ObjectViewConfig.GetEnemy(enemyType);
        spawnDirection = Random.insideUnitCircle.normalized;
        spawnPoint = spawnDirection * _balanceStorage.EnemiesConfig.EnemySpawnRadius;
        var variance = Random.Range(-_balanceStorage.EnemiesConfig.TrajectoryVariance,
            _balanceStorage.EnemiesConfig.TrajectoryVariance);
        rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        return enemyView;
    }

    private void InstantiateAsteroidParticle(Transform transform)
    {
        var asteroidParticle = _balanceStorage.ObjectViewConfig.GetEnemy(EnemyType.AsteroidParticle);

        for (int i = 0; i < _balanceStorage.EnemiesConfig.EnemyParticlesCount; i++)
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle;
            var particle = _diContainer.InstantiatePrefabForComponent<EnemyView>(asteroidParticle, position,
                transform.rotation, transform.parent);
            var enemyController = _diContainer.InstantiateComponent<EnemyController>(particle.gameObject);
            enemyController.SetDirection(Random.insideUnitCircle.normalized);
        }
    }
}