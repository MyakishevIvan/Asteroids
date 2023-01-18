using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyView : MonoBehaviour
    {
        public BaseEnemyFacede Facede { get; private set; }

        private void Awake()
        {
            Facede = GetComponent<BaseEnemyFacede>();
        }

        public void Move()
        {
            Facede.Move();
        }
    }
}