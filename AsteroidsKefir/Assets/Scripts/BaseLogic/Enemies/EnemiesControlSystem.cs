using System;
using Asteroids.Enums;
using Asteroids.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Enemies
{
    public class EnemiesControlSystem
    {
        public static event Action<Transform> OnAsteroidDamage;

        private Transform _playerTransform;

        public EnemiesControlSystem(Transform playerViewTransform)
        {
            _playerTransform = playerViewTransform;
        }

        public void UpdateAsteroidMovement(Transform transform, Vector3 direction, float speed)
        {
            var dir = direction * (speed * Time.deltaTime);

            transform.position = new Vector2
                (transform.position.x + dir.x, transform.position.y + dir.y);
        }

        public void UpdateSaucerMovement(Transform transform, float speed)
        {
            if (_playerTransform != null)
                transform.position =
                    Vector3.MoveTowards(transform.position, _playerTransform.position, speed * Time.deltaTime);
        }

        public void DamageEnemy(EnemyType enemyType, WeaponType weaponType, EnemyController enemyController)
        {
            if (enemyType == EnemyType.Asteroid && weaponType != WeaponType.Ray)
                OnAsteroidDamage?.Invoke(enemyController.transform);

            Object.Destroy(enemyController.gameObject);
            PlayerHudParams.Instance.Score++;
        }
    }
}