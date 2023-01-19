using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFactory : IEnemyFactory
    {
        [Inject] private SaucerFacade.SaucerPool _pool;
        
        public BaseEnemyFacade Creat(ITrajectorySettings trajectorySettings)
        {
            return _pool.Spawn(trajectorySettings);
        }
    }
}