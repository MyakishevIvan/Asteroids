using UnityEngine;

namespace Asteroids.Enemies
{
    public interface IEnemyTrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
    }
}