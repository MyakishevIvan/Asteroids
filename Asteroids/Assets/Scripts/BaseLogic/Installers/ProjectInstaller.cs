using Asteroids.Audio;
using Asteroids.Configs;
using Asteroids.Signals;
using Asteroids.UI.Windows;
using Asteroids.WindowsManager;
using UnityEngine;
using Zenject;

namespace Project.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private WindowsManager windowsManager;
        [SerializeField] private BalanceStorage balanceStorage;
        [SerializeField] private SoundsController soundsController;
        
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
            Container.DeclareSignal<RayEndedSignal>();
            Container.DeclareSignal<RayReloadTimeEndedSingal>();
            Container.DeclareSignal<RemoveEnemyFromActiveListSignal>();
            Container.DeclareSignal<RemoveWeaponFromActiveListSignal>();
            Container.DeclareSignal<InсreaceScoreSignal>();
        }

        private void BindSystems()
        {
            Container.Bind<WindowsManager>().FromComponentInNewPrefab(windowsManager).AsSingle().NonLazy();
            WindowsManager.CustomWindowInstantiator = CustomWindowInstantiation;
            Container.BindInterfacesAndSelfTo<BalanceStorage>().FromInstance(balanceStorage).AsSingle().NonLazy();
            Container.Bind<SoundsController>().FromInstance(soundsController).AsSingle().NonLazy();
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
