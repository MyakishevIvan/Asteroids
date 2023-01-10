using System.Collections.Generic;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Player;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = "ObjectConfig", menuName = "Configs/ObjectConfig")]
    public class ObjectViewConfig : ScriptableObject
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private List<EnemyView> enemies;
        private Dictionary<EnemyType, EnemyView> _enemyDict;
        
        public PlayerView PlayerView => playerView;

        public EnemyView GetEnemyView(EnemyType enemyType)
        {
            if (_enemyDict != null) 
                return _enemyDict[enemyType];
            
            _enemyDict = new Dictionary<EnemyType, EnemyView>();
            
            foreach (var enemy in enemies)
                _enemyDict.Add(enemy.EnemyType, enemy);

            return _enemyDict[enemyType];
        }
    }

    
}