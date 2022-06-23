using System.Collections;
using System.Collections.Generic;
using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Helper;
using UnityEngine;

public class EnemySpawner
{
    private EnemiesConfig _enemiesConfig;
    private ObjectViewConfig _objectViewConfig;
    private Transform _playerTransform;
    private GameObject _enemiesContainer;
    private EnemiesControlSystem _enemiesControlSystem;

    public EnemySpawner(Transform playerTransform)
    {
        _enemiesConfig = BalanceStorage.instance.EnemiesConfig;
        _objectViewConfig = BalanceStorage.instance.ObjectViewConfig;
        _enemiesContainer = new GameObject("EnemyContainer");
        _enemiesControlSystem = new EnemiesControlSystem(playerTransform);
        
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
            yield return new WaitForSeconds(_enemiesConfig.EnemySpawnDelay);
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < _enemiesConfig.EnemySpawnCount; i++)
        {
            var enemyType = Random.value <= _enemiesConfig.SaucerSpawnChance
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
        var enemyObj = Object.Instantiate(enemyView, spawnPoint, rotation, _enemiesContainer.transform);
        var direction = rotation * -spawnDirection;

        var enemyController = enemyObj.gameObject.AddComponent<EnemyController>();

        if (enemyType == EnemyType.Asteroid || enemyType == EnemyType.AsteroidParticle)
            enemyController.InitEnemyController(_enemiesControlSystem, direction);
        else if(enemyType == EnemyType.Saucer)
            enemyController.InitEnemyController(_enemiesControlSystem);
    }

    private EnemyView GetEnemyInstantiateParams(EnemyType enemyType, out Vector2 spawnDirection, out Vector2 spawnPoint,
        out Quaternion rotation)
    {
        var enemyView = _objectViewConfig.GetEnemy(enemyType);
        spawnDirection = Random.insideUnitCircle.normalized;
        spawnPoint = spawnDirection * _enemiesConfig.EnemySpawnRadius;
        var variance = Random.Range(-_enemiesConfig.TrajectoryVariance, _enemiesConfig.TrajectoryVariance);
        rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        return enemyView;
    }
    
    private void InstantiateAsteroidParticle(Transform transform)
    {
        Debug.LogError("BUN");
        var asteroidParticle = _objectViewConfig.GetEnemy(EnemyType.AsteroidParticle);
        
        for (int i = 0; i < _enemiesConfig.EnemyParticlesCount; i++)
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle;
            var particle = Object.Instantiate(asteroidParticle, position, transform.rotation, transform.parent);
           var enemyController = particle.gameObject.AddComponent<EnemyController>();
           enemyController.InitEnemyController(_enemiesControlSystem, Random.insideUnitCircle.normalized);
        }
    }
}