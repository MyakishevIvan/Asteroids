using System;
using Asteroids.Player.Weapon;
using UnityEngine;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyController : MonoBehaviour
    {
        protected BaseEnemy _enemy;

        protected void Awake()
        {
            _enemy = GetComponent<BaseEnemy>();
        }


        public abstract void SetTrajectorySettings(EnemyTrajectorySettings settings);
        public abstract void Move();
        public abstract void TakeDamage(BaseWeapon weapon);
    }
}