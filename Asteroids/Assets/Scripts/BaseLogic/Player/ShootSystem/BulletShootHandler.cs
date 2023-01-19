using Zenject;

namespace Player.ShootSystem
{
    public class BulletShootHandler : BaseShootHandler
    {
        [Inject] private BulletFacade.BulletPool _pool;
        
        public override bool TryShoot(out BaseWeaponFacade weaponFacade)
        {
            _trajectorySettings.Init(_playerView);
            weaponFacade = _pool.Spawn(_trajectorySettings);
            return true;
        }
    }
}