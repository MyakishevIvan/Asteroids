using System;
using System.Collections;
using Asteroids.Signals;
using Enemies.View;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyController : MonoBehaviour
    {
        [Inject] protected SignalBus _signalBus;
        protected BaseEnemyView EnemyView;

        protected void Awake()
        {
            EnemyView = GetComponent<BaseEnemyView>();
        }

        protected void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnDespawned()
        {
            _signalBus.Fire(new RemoveEnemyFromActiveListSignal(this));
            gameObject.SetActive(false);
        }

        protected void OnCreated()
        {
            gameObject.SetActive(false);
        }
        
        public abstract void SetTrajectorySettings(ITrajectorySettings settings);
        public abstract void Move();
        public abstract void BulletDamage();
        public abstract void RayDamage();
        protected abstract void HitEnemy();
        public abstract void DespawnEnemy();
    }
}