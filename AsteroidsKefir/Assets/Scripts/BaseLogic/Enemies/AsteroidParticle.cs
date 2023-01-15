using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidParticle : BaseEnemy
    {
        public override void Move()
        {
        }

        public class AsteroidPool : MemoryPool<EnemyTrajectorySettings, BaseEnemy>
        {
            [Inject] private DiContainer _diContainer;

            protected override void OnCreated(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(false);
                _diContainer.InstantiateComponent<AsteroidController>(enemy.gameObject);
            }

            protected override void OnSpawned(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(true);
            }

            protected override void OnDespawned(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(false);
            }

            protected override void Reinitialize(EnemyTrajectorySettings settings, BaseEnemy enemy)
            {
                enemy.GetComponent<AsteroidParticleController>().SetTrajectorySettings(settings);
            }
        }
    }
}