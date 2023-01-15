using Asteroids.Configs;
using Asteroids.Player.Weapon;
using Zenject;

namespace Asteroids.Player.ShootSystem
{
    public class BulletShootCreator : ShootingCreator
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private BulletWeapon.BulletPool _bulletFactory;
        
        private BaseWeapon _currentBullet;
        
        public override void CreatWeapon()
        {
            if(_playerView == null)
                return;
            _currentBullet = _bulletFactory.Spawn(_playerView);
        }
    }
}