using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticleFactory : IEnemyFactory
    {
        [Inject] private AsteroidParticleFacade.AsteroidParticlePool _pool;
        
        public BaseEnemyFacade Creat(ITrajectorySettings settings)
        {
            return _pool.Spawn(settings);
        }
    }
}