using System;
using Asteroids.Configs;
using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyType _enemyType;
        private EnemiesConfig _enemiesConfig;
        private float _speed;
        private EnemiesControlSystem _enemiesControlSystem;
        private Vector3 _direction;

        private void Awake()
        {
            _enemyType = GetComponent<EnemyView>().EnemyType;
            _enemiesConfig = BalanceStorage.instance.EnemiesConfig;

            SelectEnemySpeed();
        }

        private void SelectEnemySpeed()
        {
            switch (_enemyType)
            {
                case EnemyType.Asteroid:
                    _speed = _enemiesConfig.AsteroidFlySpeed;
                    break;
                case EnemyType.Saucer:
                    _speed = _enemiesConfig.SaucerFlySpeed;
                    break;
                case EnemyType.AsteroidParticle:
                    _speed = _enemiesConfig.AsteroidParticleFlySpeed;
                    break;

                default:
                    throw new ArgumentException("There is no speed for enemy Type " + _enemyType);
            }
        }

        public void InitEnemyController(EnemiesControlSystem enemiesControlSystem)
        {
            _enemiesControlSystem = enemiesControlSystem;
        }
        
        public void InitEnemyController(EnemiesControlSystem enemiesControlSystem,  Vector3 direction)
        {
            _enemiesControlSystem = enemiesControlSystem;
            _direction = direction;
        }

        private void Update()
        {
            if (_enemyType == EnemyType.Asteroid || _enemyType == EnemyType.AsteroidParticle)
                _enemiesControlSystem.UpdateAsteroidMovement(transform, _direction, _speed);
            else if (_enemyType == EnemyType.Saucer)
                _enemiesControlSystem.UpdateSaucerMovement(transform, _speed);
        }

        public void DamageEnemy(WeaponType weaponType)
        {
            _enemiesControlSystem.DamageEnemy(_enemyType, weaponType, this);
        }
    }
}