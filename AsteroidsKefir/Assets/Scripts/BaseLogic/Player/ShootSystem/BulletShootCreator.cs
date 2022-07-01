using System;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Asteroids.Player.ShootSystem
{
    public class BulletShootCreator : ShootingCreator
    {
        [Inject] private BalanceStorage _balanceStorage;
        
        protected override void SelectWeapon()
        {
            _weapon = _balanceStorage.WeaponConfig.GetWeaponView(WeaponType.Bullet);
        }

        public BulletShootCreator() : base("Bullet")
        {
        }
    }
}