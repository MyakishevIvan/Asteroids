using Zenject;

namespace Player.ShootSystem
{
    public class BulletShootHandler : BaseShootHandler
    {
        [Inject] private BulletController.BulletPool _pool;
        
        public override bool TryShoot(out BaseWeaponController weaponController)
        {
            _trajectorySettings.Init(_playerView);
            weaponController = _pool.Spawn(_trajectorySettings);
            return true;
        }
    }
}