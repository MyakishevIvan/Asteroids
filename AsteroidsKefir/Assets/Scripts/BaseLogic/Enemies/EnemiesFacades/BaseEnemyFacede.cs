using System;
using System.Collections;
using Asteroids.Player.Weapon;
using UnityEngine;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyFacede : MonoBehaviour
    {
        protected BaseEnemyView EnemyView;

        protected void Awake()
        {
            EnemyView = GetComponent<BaseEnemyView>();
        }
        
        public abstract void SetTrajectorySettings(IEnemyTrajectorySettings settings);
        public abstract void Move();
        public abstract void BulletDamage();
        public abstract void RayDamage();
        protected abstract void HitEnemy();
        public abstract void DespawnEnemy();
    }
}