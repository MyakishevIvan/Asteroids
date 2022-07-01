using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Player;
using Asteroids.Windows;
using UnityEngine;
using Zenject;

namespace Asteroids.BaseLogic
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private WindowsManager windowsManager;
        [SerializeField] private BalanceStorage balanceStorage;
        

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = false;

            BindSystems();
            DeclareSignals();
        }

        private void BindSystems()
        {
            Container.Bind<WindowsManager>().FromComponentInNewPrefab(windowsManager).AsSingle().NonLazy();
            WindowsManager.CustomWindowInstantiator = CustomWindowInstantiation;
            Container.Bind<BalanceStorage>().FromInstance(balanceStorage).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudParams>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputAction>().AsSingle().NonLazy();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            
        }

        private Window CustomWindowInstantiation(Window windowPrefab)
        {
            var parent = Container.Resolve<WindowsManager>().transform;
            var window = Container.InstantiatePrefabForComponent<Window>(windowPrefab, parent);
            Container.Inject(window.gameObject);
            return window;
        }
    }
    
}
