using UnityEngine;

namespace Asteroids.Enemies
{
    public class EnemyTrajectorySettings : IEnemyTrajectorySettings
    {
        public Vector2 SpawnPoint { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector2 SpawnDistance { get; set; }
        public Vector2 Direction { get; set; }

        public EnemyTrajectorySettings(int enemySpawnRadius, int trajectoryVariance)
        {
            SpawnDistance = Random.insideUnitCircle.normalized;
            SpawnPoint = SpawnDistance * enemySpawnRadius;
            var variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Direction = Rotation * -SpawnDistance;
        }
    }
}