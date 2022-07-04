using Asteroids.Enums;
using UnityEngine;
using Zenject;

namespace Asteroids.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        public EnemyType EnemyType => enemyType;
        
        public class AsteroidFactory : PlaceholderFactory<EnemyView>
        {
            
        }  
        public class AsteroidParticleFactory : PlaceholderFactory<EnemyView>
        {
            
        } 
        public class SaucerFactory : PlaceholderFactory<EnemyView>
        {
            
        }
        
        
    }
}