using System;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemiesConfig), menuName = "Configs/"+nameof(EnemiesConfig))]
    public class EnemiesConfig : ScriptableObject
    {
       [SerializeField] private float saucerSpawnChance;
       [SerializeField] private int enemySpawnDelay;
       [SerializeField] private int enemySpawnCount;
       [SerializeField] private int enemySpawnRadius;
       [SerializeField] private int enemyParticlesCount;
       [SerializeField] private int saucerFlySpeed;
       [SerializeField] private int asteroidFlySpeed;
       [SerializeField] private int asteroidParticleFlySpeed;
       [SerializeField] private int lifeTime;
       [SerializeField] private int trajectoryVariance;

       public float SaucerSpawnChance => saucerSpawnChance;
       public int EnemySpawnDelay => enemySpawnDelay;
       public int EnemySpawnCount => enemySpawnCount;
       public int EnemySpawnRadius => enemySpawnRadius;
       public int EnemyParticlesCount => enemyParticlesCount;
       public int SaucerFlySpeed => saucerFlySpeed;
       public int AsteroidFlySpeed => asteroidFlySpeed;
       public int AsteroidParticleFlySpeed => asteroidParticleFlySpeed;
       public int LifeTime => lifeTime;
       public int TrajectoryVariance => trajectoryVariance;
    }
    
}