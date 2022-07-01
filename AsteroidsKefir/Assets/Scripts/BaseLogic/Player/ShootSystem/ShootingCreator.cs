using Asteroids.Configs;
using Asteroids.Player.Weapon;
using Asteroids.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Player.ShootSystem
{
    public class ShootingCreator : IInitializable
    {
        [Inject] private PlayerView _playerView;
        [Inject] private DiContainer _diContainer;
        [Inject] private PlayerHudParams _playerHudParams;
        [Inject] private BalanceStorage _balanceStorage;
        protected WeaponView _weapon;
        private GameObject _weaponContainer;
        private string _containerName;

        public ShootingCreator(string containerName)
        {
            _containerName = containerName;
        }
        
        public void Initialize()
        {
            _playerHudParams.rayCount = _balanceStorage.WeaponConfig.RayShootCount;
            _playerHudParams.rayReloadTime = 0;
            _weaponContainer = new GameObject(_containerName+"Container");
            _playerHudParams.rayCount = _balanceStorage.WeaponConfig.RayShootCount;
            SelectWeapon();
        }

        protected virtual void SelectWeapon()
        {
        }

        public virtual void Shoot()
        {
            if(_playerView == null)
                return;
            
            var weaponObj = Object.Instantiate
            (_weapon, _playerView.transform.position,
                Quaternion.Euler(0, 0, _playerView.transform.rotation.eulerAngles.z),
                _weaponContainer.transform);

            var weaponController = _diContainer.InstantiateComponent<WeaponController>(weaponObj.gameObject);
            weaponController.AddDirection(_playerView.transform.up);
        }
    }
}