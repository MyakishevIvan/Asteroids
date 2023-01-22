using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName =nameof(WeaponConfig), menuName = "Configs/"+nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int _rayShootCount;
        [SerializeField] private int _reloadTime;
        [SerializeField] private int _bulletShootSpeed;
        [SerializeField] private int _rayShootSpeed;
        [SerializeField] private int _shootDelay;
        [SerializeField] private int _weaponLifeTime;

        public int RayShootCount => _rayShootCount;
        public int ReloadTime => _reloadTime;
        public int BulletShootSpeed => _bulletShootSpeed;
        public int RayShootSpeed => _rayShootSpeed;
        public int ShootDelay => _shootDelay;
        public int WeaponLifeTime => _weaponLifeTime;
    }
}