namespace Asteroids.Enemies
{
    public interface IEnemyFactory
    {
        BaseEnemyController Creat(ITrajectorySettings settings);
    }
}