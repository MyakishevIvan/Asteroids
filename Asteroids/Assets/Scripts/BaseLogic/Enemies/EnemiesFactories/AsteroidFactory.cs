using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidFactory : IEnemyFactory
    {
        [Inject] private AsteroidController.AsteroidPool _pool;
        
        public BaseEnemyController Creat(ITrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}