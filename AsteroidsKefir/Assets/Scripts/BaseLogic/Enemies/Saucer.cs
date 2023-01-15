using Zenject;

namespace Asteroids.Enemies
{
    public class Saucer : BaseEnemy
    {
        private BaseEnemyController _controller;
        private void Start()
        {
            _controller = GetComponent<SaucerController>();
        }

        public override void Move()
        {
            _controller.Move();
        }
        
        public class SaucerPool : MemoryPool<EnemyTrajectorySettings, BaseEnemy>
        {
            [Inject] private DiContainer _diContainer;
            protected override void OnCreated(BaseEnemy enemy)
            {
                enemy.gameObject.SetActive(false);
                _diContainer.InstantiateComponent<SaucerController>(enemy.gameObject);
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
                enemy.GetComponent<SaucerController>().SetTrajectorySettings(settings);
            }
        }
    }
}