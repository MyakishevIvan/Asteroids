using Asteroids.Enemies;
using UnityEngine;

namespace Asteroids.Signals
{
    public class AsteroidBlowSignal
    {
        public Transform AsroidTransform { get; }

        public AsteroidBlowSignal(Transform asteroidAsroidTransform)
        {
            AsroidTransform = asteroidAsroidTransform;
        }
    }

    public class StartGameSignal
    {
        
    }

    public class EndGameSignal
    {
        
    }
    
    public class RayEndedSignal
    {
        
    }
    
    public class RayReloadTimeEned
    {
        
    }

    public class RemoveEnemyFromActiveList
    {
        public BaseEnemy Enemy { get; }
        public RemoveEnemyFromActiveList(BaseEnemy enemy)
        {
            Enemy = enemy;
        }
    }
}