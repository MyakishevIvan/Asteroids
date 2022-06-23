using System.Collections.Generic;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName =nameof(WeaponConfig), menuName = "Configs/"+nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponView> weaponsList;
        private Dictionary<WeaponType, WeaponView> _weaponsDict;
        [SerializeField] private int _rayShootCount;
        [SerializeField] private int _reloadTime;
        [SerializeField] private int _shootSpeed;
        [SerializeField] private int _shootDelay;

        public int RayShootCount => _rayShootCount;
        public int ReloadTime => _reloadTime;
        public int ShootSpeed => _shootSpeed;
        public int ShootDelay => _shootDelay;
        
        public WeaponView GetWeaponView(WeaponType weaponType)
        {
            if (_weaponsDict == null)
            {
                _weaponsDict = new Dictionary<WeaponType, WeaponView>();
                foreach (var weapon in weaponsList)
                {
                    _weaponsDict.Add(weapon.WeaponType, weapon);
                }
            }

            return _weaponsDict[weaponType];
        }
        
    }
}