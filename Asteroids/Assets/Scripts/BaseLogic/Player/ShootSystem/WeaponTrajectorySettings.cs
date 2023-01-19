using Asteroids.Enemies;
using Player.Stats;
using UnityEngine;

namespace Player.ShootSystem
{
    public class WeaponTrajectorySettings : ITrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
        public Vector2 Direction { get; set; }
        public Quaternion Rotation { get; set; }

        public void Init(PlayerView player)
        {
            SpawnPoint = player.transform.position;
            Direction = player.transform.up;
            Rotation = Quaternion.Euler(0, 0, player.transform.rotation.eulerAngles.z);
        }
    }
}