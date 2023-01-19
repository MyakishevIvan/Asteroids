using Asteroids.Enemies;
using Player.Stats;
using Zenject;

namespace Player.ShootSystem
{
    public abstract class BaseShootHandler : IInitializable
    {
        [Inject] protected PlayerView _playerView;
        protected WeaponTrajectorySettings _trajectorySettings;
        
        public void Initialize()
        {
            _trajectorySettings = new WeaponTrajectorySettings();
        }
        
        public abstract bool TryShoot(out BaseWeaponFacade weaponFacade);
    }
}