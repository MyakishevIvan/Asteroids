using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Enemies
{
    public abstract class BaseEnemyView : MonoBehaviour
    {
        public BaseEnemyFacade Facade { get; private set; }

        private void Awake()
        {
            Facade = GetComponent<BaseEnemyFacade>();
        }

        public void Move()
        {
            Facade.Move();
        }
    }
}