using Asteroids.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.ShootSystem
{
    public abstract class ShootingCreator
    {
        [Inject] protected PlayerView _playerView;
        [Inject] private DiContainer _diContainer;
        
        public abstract void CreatWeapon();
        
        protected void Shoot(BaseWeapon baseWeapon)
        {
            if(_playerView == null)
                return;
            
            baseWeapon.transform.position = _playerView.transform.position;
            baseWeapon.transform.rotation = Quaternion.Euler(0, 0, _playerView.transform.rotation.eulerAngles.z);
            var weaponController = _diContainer.InstantiateComponent<WeaponController>(baseWeapon.gameObject);
            weaponController.Init(_playerView.transform.up);
        }
    }
}