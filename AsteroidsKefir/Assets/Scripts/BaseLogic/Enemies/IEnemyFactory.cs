namespace Asteroids.Enemies
{
    public interface IEnemyFactory
    {
        BaseEnemyFacede Creat(IEnemyTrajectorySettings settings);
    }
}