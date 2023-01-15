using Zenject;

namespace Asteroids.Enemies
{
    public class SaucerFactory : IEnemyFactory
    {
        [Inject] private Saucer.SaucerPool _pool;
        
        public BaseEnemy Creat(EnemyTrajectorySettings trajectorySettings)
        {
            return _pool.Spawn(trajectorySettings);
        }
    }
}