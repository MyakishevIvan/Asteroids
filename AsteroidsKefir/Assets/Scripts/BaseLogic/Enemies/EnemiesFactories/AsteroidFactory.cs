using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidFactory : IEnemyFactory
    {
        [Inject] private AsteroidFacade.AsteroidPool _pool;
        
        public BaseEnemyFacede Creat(IEnemyTrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}