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

    public class InсreaceScoreSignal
    {
        
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
    
    public class RayReloadTimeEndedSingal
    {
        
    }

    public class RemoveEnemyFromActiveListSignal
    {
        public BaseEnemyController EnemyController { get; }
        public RemoveEnemyFromActiveListSignal(BaseEnemyController enemyController)
        {
            EnemyController = enemyController;
        }
    }
    
    public class RemoveWeaponFromActiveListSignal
    {
        public BaseWeaponController WeaponController { get; }
        public RemoveWeaponFromActiveListSignal(BaseWeaponController weaponController)
        {
            WeaponController = weaponController;
        }
    }
}