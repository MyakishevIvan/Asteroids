using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidFactory : IEnemyFactory
    {
        [Inject] private AsteroidFacade.AsteroidPool _pool;
        
        public BaseEnemyFacade Creat(ITrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}