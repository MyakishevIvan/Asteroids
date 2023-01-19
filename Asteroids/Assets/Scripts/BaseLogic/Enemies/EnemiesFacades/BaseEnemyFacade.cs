using System;
using System.Collections;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyFacade : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        protected BaseEnemyView EnemyView;

        protected void Awake()
        {
            EnemyView = GetComponent<BaseEnemyView>();
        }

        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            _signalBus.Fire(new RemoveEnemyFromActiveList(this));
            gameObject.SetActive(false);
        }

        public void OnCreated()
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