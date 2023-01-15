using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleFactory : IEnemyFactory
    {
        [Inject] private AsteroidParticle.AsteroidPool _pool;
        
        public BaseEnemy Creat(EnemyTrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}