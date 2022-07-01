using System;
using Asteroids.Enums;
using Asteroids.Player;
using Asteroids.Windows;
using UnityEngine;
using Object = UnityEngine.Object;
using Zenject;

namespace Asteroids.Enemies
{
    public class EnemiesControlSystem
    {
        [Inject] private PlayerHudParams _playerHudParams;
        [Inject] private PlayerView _playerView;
        public static event Action<Transform> OnAsteroidDamage;
       
       
        public void UpdateAsteroidMovement(Transform transform, Vector3 direction, float speed)
        {
            var dir = direction * (speed * Time.deltaTime);

            transform.position = new Vector2
                (transform.position.x + dir.x, transform.position.y + dir.y);
        }

        public void UpdateSaucerMovement(Transform transform, float speed)
        {
            if (_playerView != null)
                transform.position =
                    Vector3.MoveTowards(transform.position, _playerView.transform.position, speed * Time.deltaTime);
        }

        public void DamageEnemy(EnemyType enemyType, WeaponType weaponType, EnemyController enemyController)
        {
            if (enemyType == EnemyType.Asteroid && weaponType != WeaponType.Ray)
                OnAsteroidDamage?.Invoke(enemyController.transform);

            Object.Destroy(enemyController.gameObject);
            _playerHudParams.Score++;
        }
    }
}