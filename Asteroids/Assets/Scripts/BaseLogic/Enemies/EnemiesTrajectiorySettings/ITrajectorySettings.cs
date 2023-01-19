using UnityEngine;

namespace Asteroids.Enemies
{
    public interface ITrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
        public Vector2 Direction { get; set; }
    }
}