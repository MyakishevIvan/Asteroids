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
        [SerializeField] private List<BaseEnemyView> _enemyViews;
        [SerializeField] private GameObject asteroid;
        [SerializeField] private GameObject saucer;
        [SerializeField] private GameObject asteroidParticle;
        private Dictionary<Type, GameObject> _enemiesDictionary;

        public PlayerView PlayerView => playerView;
        
        
        public GameObject GetEnemy<T>() where T : BaseEnemyView
        {
            if (_enemiesDictionary != null)
                return _enemiesDictionary[typeof(T)];

            _enemiesDictionary = new Dictionary<Type, GameObject>();
            
            foreach (var enemyView in _enemyViews)
                _enemiesDictionary.Add(enemyView.GetType(), enemyView.gameObject);

            return _enemiesDictionary[typeof(T)];
        }
    }
}