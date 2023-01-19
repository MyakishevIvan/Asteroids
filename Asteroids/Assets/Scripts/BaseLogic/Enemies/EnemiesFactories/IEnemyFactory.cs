namespace Asteroids.Enemies
{
    public interface IEnemyFactory
    {
        BaseEnemyFacade Creat(ITrajectorySettings settings);
    }
}