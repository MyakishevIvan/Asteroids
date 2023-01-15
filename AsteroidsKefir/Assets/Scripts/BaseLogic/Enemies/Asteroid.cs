using Zenject;

namespace Asteroids.Enemies
{
    public class Asteroid : BaseEnemy
    {
        private BaseEnemyController _controller;

        private void Start()
        {
            _controller = GetComponent<AsteroidController>();
        }
        
        public override void Move()
        {
            _controller.Move();
        }

        public class AsteroidPool : MemoryPool<EnemyTrajectorySettings, BaseEnemy>
        {
            [Inject] private DiContainer _diContainer;

            // Called immediately after the item is first added to the pool
            protected override void OnCreated(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(false);
                _diContainer.InstantiateComponent<AsteroidController>(enemy.gameObject);
            }

            // Called immediately after the item is removed from the pool
            protected override void OnSpawned(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(true);
            }

            // Called immediately after the item is returned to the pool
            protected override void OnDespawned(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(false);
            }

            // Similar to OnSpawned
            // Called immediately after the item is removed from the pool
            // This method will also contain any parameters that are passed along
            // to the memory pool from the spawning code
            protected override void Reinitialize(EnemyTrajectorySettings settings, BaseEnemy enemy)
            {
                enemy.GetComponent<AsteroidController>().SetTrajectorySettings(settings);
            }
        }
    }
}