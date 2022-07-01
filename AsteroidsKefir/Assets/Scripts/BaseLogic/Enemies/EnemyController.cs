using System;
using Asteroids.Configs;
using Asteroids.Enums;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] private EnemiesControlSystem _enemiesControlSystem;
        [Inject] private BalanceStorage _balanceStorage;
        private EnemyType _enemyType;
        private float _speed;
        private Vector3 _direction;

        private void Start()
        {
            
            _enemyType = GetComponent<EnemyView>().EnemyType;
            SelectEnemySpeed();
        }

        private void SelectEnemySpeed()
        {
            switch (_enemyType)
            {
                case EnemyType.Asteroid:
                    _speed = _balanceStorage.EnemiesConfig.AsteroidFlySpeed;
                    break;
                case EnemyType.Saucer:
                    _speed = _balanceStorage.EnemiesConfig.SaucerFlySpeed;
                    break;
                case EnemyType.AsteroidParticle:
                    _speed = _balanceStorage.EnemiesConfig.AsteroidParticleFlySpeed;
                    break;

                default:
                    throw new ArgumentException("There is no speed for enemy Type " + _enemyType);
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
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