using UnityEngine;

namespace Asteroids.Enemies
{
    public class AsteroidParticleTrajectorySettings : ITrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
        public Vector2 Direction { get; set; }

        public AsteroidParticleTrajectorySettings()
        {
            SpawnPoint =Random.insideUnitCircle;
            Direction = Random.insideUnitCircle.normalized;
        }

        public void SetExplodedPosition(Vector2 position)
        {
            SpawnPoint += position;
        }
    }
}