using System.Collections.Generic;
using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName =nameof(WeaponConfig), menuName = "Configs/"+nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        private Dictionary<WeaponType, GameObject> _weaponsDict;
        
        [SerializeField] private int _rayShootCount;
        [SerializeField] private int _reloadTime;
        [SerializeField] private int _bulletShootSpeed;
        [SerializeField] private int _rayShootSpeed;
        [SerializeField] private int _shootDelay;

        public int RayShootCount => _rayShootCount;
        public int ReloadTime => _reloadTime;
        public int BulletShootSpeed => _bulletShootSpeed;
        public int RayShootSpeed => _rayShootSpeed;
        public int ShootDelay => _shootDelay;
    }
}