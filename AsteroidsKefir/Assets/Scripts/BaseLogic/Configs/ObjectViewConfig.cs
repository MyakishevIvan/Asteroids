using System;
using System.Collections.Generic;
using Asteroids.Enemies;
using Asteroids.Player;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = "ObjectConfig", menuName = "Configs/ObjectConfig")]
    public class ObjectViewConfig : ScriptableObject
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private GameObject asteroid;
        [SerializeField] private GameObject saucer;
        [SerializeField] private GameObject asteroidParticle;
        private Dictionary<Type, GameObject> _enemiesDictionary;

        public PlayerView PlayerView => playerView;
        
        public GameObject GetEnemy<T>() where T : BaseEnemy
        {
            if (_enemiesDictionary != null)
                return _enemiesDictionary[typeof(T)];

            _enemiesDictionary = new Dictionary<Type, GameObject>()
            {
                {typeof(Asteroid), asteroid},
                {typeof(Saucer), saucer},
                {typeof(AsteroidParticle), asteroidParticle}

            };
            

            return _enemiesDictionary[typeof(T)];
        }
    }
}