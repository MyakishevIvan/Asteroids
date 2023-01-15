using Asteroids.Configs;
using Asteroids.Player.Weapon;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class AsteroidController : BaseEnemyController
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private Asteroid.AsteroidPool _pool;
        [Inject] private BalanceStorage _balanceStorage;
        private Vector2 _direction;
        private Vector2 _currentDirection;
        
        private float _speed;

        private new void Awake()
        {
            base.Awake();
            _speed = _balanceStorage.EnemiesConfig.AsteroidFlySpeed;
        }
        
        public override void SetTrajectorySettings(EnemyTrajectorySettings settings)
        {
            transform.position = settings.SpawnPoint;
            _direction = settings.Rotation * -settings.SpawnDistance;
        }

        public override void Move()
        {
            _currentDirection = _direction * (_speed * Time.deltaTime);
            transform.position = new Vector2
                (transform.position.x + _currentDirection.x, transform.position.y + _currentDirection.y);
        }

        public override void TakeDamage(BaseWeapon weapon)
        {
            _signalBus.Fire(new RemoveEnemyFromActiveList(_enemy));
            _pool.Despawn(GetComponent<Asteroid>());
        }
    }
}