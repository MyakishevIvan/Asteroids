using Asteroids.Signals;
using Zenject;

namespace Asteroids.Enemies
{
    public class EnemiesManager : ITickable
    {
        private EnemiesSpawner _enemiesSpawner;
        private SignalBus _signalBus;
        
        public EnemiesManager(SignalBus signalBus, EnemiesSpawner enemiesSpawner)
        {
            _enemiesSpawner = enemiesSpawner;
            _signalBus = signalBus;
            _signalBus.Subscribe<RemoveEnemyFromActiveListSignal>(RemoveDespawnedEnemyFromList);
            _signalBus.Subscribe<AsteroidBlowSignal>(SpawnAsteroidParticle);
        }

        private void RemoveDespawnedEnemyFromList(RemoveEnemyFromActiveListSignal signal) =>
            _enemiesSpawner.RemoveDespawnedEnemyFromList(signal.EnemyController);
        
        private void SpawnAsteroidParticle(AsteroidBlowSignal signal) => _enemiesSpawner.SpawnAsteroidParticle(signal.AsroidTransform);
        
        public void StartSpawnEnemies() => _enemiesSpawner.StartSpawnEnemies();
        
        public void StopSpawnAndDespawnEnemies() => _enemiesSpawner.StopSpawnAndDespawnEnemies();

        public void Tick()
        {
            foreach (var enemy in _enemiesSpawner)
                enemy.Move();
        }
    }
}