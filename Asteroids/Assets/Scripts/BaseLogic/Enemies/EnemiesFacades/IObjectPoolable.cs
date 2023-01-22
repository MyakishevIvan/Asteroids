namespace Asteroids.Enemies
{
    public interface IObjectPoolable
    {
        void OnSpawned();
        void OnCreated();
        void OnDespawned();
    }
}