using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleFactory : IEnemyFactory
    {
        [Inject] private AsteroidParticleFacade.AsteroidParticlePool _pool;
        
        public BaseEnemyFacede Creat(IEnemyTrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}