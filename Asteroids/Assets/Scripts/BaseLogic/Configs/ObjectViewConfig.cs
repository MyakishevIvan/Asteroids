﻿using System;
using System.Collections.Generic;
using Enemies.View;
using Player.View;
using UnityEngine;
using Weapons.View;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(ObjectViewConfig), menuName = "Configs/" +nameof(ObjectViewConfig))]
    public class ObjectViewConfig : ScriptableObject
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private List<BaseEnemyView> _enemiesViews;
        [SerializeField] private List<BaseWeaponView> _weaponsViews;

        private Dictionary<Type, GameObject> _enemiesViewsDictionary;
        private Dictionary<Type, GameObject> _weaponsViewsDictionary;
        
        public PlayerView PlayerView => playerView;
        
        public GameObject GetEnemyView<T>() where T : BaseEnemyView
        {
            if (_enemiesViewsDictionary != null)
                return _enemiesViewsDictionary[typeof(T)];

            _enemiesViewsDictionary = new Dictionary<Type, GameObject>();

            foreach (var enemyView in _enemiesViews)
                _enemiesViewsDictionary.Add(enemyView.GetType(), enemyView.gameObject);

            return _enemiesViewsDictionary[typeof(T)];
        }
        
        public GameObject GetWeaponView<T>() where T : BaseWeaponView
        {
            if (_weaponsViewsDictionary != null)
                return _weaponsViewsDictionary[typeof(T)];

            _weaponsViewsDictionary = new Dictionary<Type, GameObject>();

            foreach (var enemyView in _weaponsViews)
                _weaponsViewsDictionary.Add(enemyView.GetType(), enemyView.gameObject);

            return _weaponsViewsDictionary[typeof(T)];
        }
    }
}