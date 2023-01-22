using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFactory : IEnemyFactory
    {
        [Inject] private SaucerController.SaucerPool _pool;
        
        public BaseEnemyController Creat(ITrajectorySettings trajectorySettings)
        {
            return _pool.Spawn(trajectorySettings);
        }
    }
}