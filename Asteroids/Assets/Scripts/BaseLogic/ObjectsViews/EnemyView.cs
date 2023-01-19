using System;
using Asteroids.Enums;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private EnemyType enemyType;
        private IMemoryPool _pool;
        public EnemyType EnemyType => enemyType;

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }
    }
}