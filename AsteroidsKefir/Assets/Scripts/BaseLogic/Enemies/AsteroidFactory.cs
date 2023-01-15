using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidFactory : IEnemyFactory
    {
        [Inject] private Asteroid.AsteroidPool _pool;
        
        public BaseEnemy Creat(EnemyTrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}