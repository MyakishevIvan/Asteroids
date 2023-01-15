using System.Collections.Generic;
using Asteroids.Enums;
using Asteroids.Player.Weapon;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName =nameof(WeaponConfig), menuName = "Configs/"+nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private GameObject BulletObject;
        [SerializeField] private GameObject RayObject;
        
        private Dictionary<WeaponType, GameObject> _weaponsDict;
        
        [SerializeField] private int _rayShootCount;
        [SerializeField] private int _reloadTime;
        [SerializeField] private int _shootSpeed;
        [SerializeField] private int _shootDelay;

        public int RayShootCount => _rayShootCount;
        public int ReloadTime => _reloadTime;
        public int ShootSpeed => _shootSpeed;
        public int ShootDelay => _shootDelay;
        
        public GameObject GetWeaponView(WeaponType weaponType)
        {
            if (_weaponsDict != null)
                return _weaponsDict[weaponType];
            
            _weaponsDict = new Dictionary<WeaponType, GameObject>()
            {
                { WeaponType.Bullet , BulletObject},
                { WeaponType.Ray , RayObject}
            };
            
         

            return _weaponsDict[weaponType];
        }
        
    }
}