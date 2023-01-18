using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFactory : IEnemyFactory
    {
        [Inject] private SaucerFacade.SaucerPool _pool;
        
        public BaseEnemyFacede Creat(IEnemyTrajectorySettings trajectorySettings)
        {
            return _pool.Spawn(trajectorySettings);
        }
    }
}