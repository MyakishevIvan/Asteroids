namespace Asteroids.Enemies
{
    public interface IEnemyFactory
    {
        BaseEnemy Creat(EnemyTrajectorySettings settings);
    }
}