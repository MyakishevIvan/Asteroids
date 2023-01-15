using System;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] private EnemiesControlSystem _enemiesControlSystem;
        [Inject] private BalanceStorage _balanceStorage;
        public EnemyView EnemyView { get; private set; }
        private float _speed;
        private Vector3 _direction;

        private void Start()
        {
            EnemyView = GetComponent<EnemyView>();
            SelectEnemySpeed();
        }

        private void SelectEnemySpeed()
        {
            switch (EnemyView.EnemyType)
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
                    throw new ArgumentException("There is no speed for enemy Type " + EnemyView.EnemyType);
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            if (EnemyView.EnemyType == EnemyType.Asteroid || EnemyView.EnemyType == EnemyType.AsteroidParticle)
                _enemiesControlSystem.UpdateAsteroidMovement(transform, _direction, _speed);
            else if (EnemyView.EnemyType == EnemyType.Saucer)
                _enemiesControlSystem.UpdateSaucerMovement(transform, _speed);
        }

        public void DamageEnemy(BaseWeapon weapon)
        {
            // _enemiesControlSystem.DamageEnemy(EnemyView.EnemyType, weaponType, this);
        }
    }
}