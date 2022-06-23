using System;
using Asteroids.Player.Weapon;

namespace Asteroids.Player.ShootSystem
{
    public class ShootingCreator
    {
        private Action<WeaponView> OnShoot;
        private WeaponView _weapon;

        protected ShootingCreator(WeaponView weapon, Action<WeaponView> OnShoot)
        {
            _weapon = weapon;
            this.OnShoot = OnShoot;
        }

        public virtual void Shoot()
        {
            OnShoot?.Invoke(_weapon);
        }
    }
}