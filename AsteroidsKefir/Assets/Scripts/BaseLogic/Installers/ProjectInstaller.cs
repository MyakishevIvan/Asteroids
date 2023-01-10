using Asteroids.Configs;
using Asteroids.Windows;
using Asteroids.Signals;
using BaseLogic.Controllers;
using UnityEngine;
using Zenject;

namespace BaseLogic.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private WindowsManager windowsManager;
        [SerializeField] private BalanceStorage balanceStorage;
        
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = false;
            DeclareSignals();
            BindSystems();
        }
        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AsteroidBlowSignal>();
            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<EndGameSignal>();
        }

        private void BindSystems()
        {
            Container.Bind<WindowsManager>().FromComponentInNewPrefab(windowsManager).AsSingle().NonLazy();
            WindowsManager.CustomWindowInstantiator = CustomWindowInstantiation;
            Container.Bind<BalanceStorage>().FromInstance(balanceStorage).AsSingle().NonLazy();
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
