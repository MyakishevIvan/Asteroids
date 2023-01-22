using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleFactory : IEnemyFactory
    {
        [Inject] private AsteroidParticleController.AsteroidParticlePool _pool;
        
        public BaseEnemyController Creat(ITrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}