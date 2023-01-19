using Asteroids.Enemies;
using Player.ShootSystem;
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
        public BaseEnemyFacade EnemyFacade { get; }
        public RemoveEnemyFromActiveList(BaseEnemyFacade enemyFacade)
        {
            EnemyFacade = enemyFacade;
        }
    }
    
    public class RemoveWeaponFromActiveList
    {
        public BaseWeaponFacade WeaponFacade { get; }
        public RemoveWeaponFromActiveList(BaseWeaponFacade weaponFacade)
        {
            WeaponFacade = weaponFacade;
        }
    }
}