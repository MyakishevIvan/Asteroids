using System;
using Asteroids.Player.Weapon;

namespace Asteroids.Player.ShootSystem
{
    public class BulletShootCreator : ShootingCreator
    {
        public BulletShootCreator(WeaponView weapon, Action<WeaponView> OnShoot) : base(weapon, OnShoot)
        {
        }
    }
}