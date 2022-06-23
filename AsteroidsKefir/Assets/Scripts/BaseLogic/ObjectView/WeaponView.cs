using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Player.Weapon
{
    [RequireComponent(typeof(SpriteRenderer))]
    public  class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        public WeaponType WeaponType => weaponType;
    }
}