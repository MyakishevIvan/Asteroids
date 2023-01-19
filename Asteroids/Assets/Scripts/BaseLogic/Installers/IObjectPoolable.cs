namespace BaseLogic.Installers
{
    public interface IObjectPoolable
    {
        void OnSpawned();
        void OnCreated();
        void OnDespawned();
    }
}