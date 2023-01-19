using Asteroids.Configs;
using Asteroids.Enemies;
using Player.Stats;
using BaseLogic.Controllers;
using UnityEngine;
using Zenject;
using Asteroids.GameProfile;
using BaseLogic.ObjectView;
using Player.Controller;
using Player.MovementSystem;
using Player.ShootSystem;

namespace BaseLogic.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private SceneContext sceneContext;
        [SerializeField] private Transform horizontalBounds;
        [SerializeField] private Transform verticalBounds;
        [Inject] private BalanceStorage _balanceStorage;

        public override void InstallBindings()
        {
            InstantiatePools();
            var playerViewInstance = Container.InstantiatePrefabForComponent<PlayerView>(
                _balanceStorage.ObjectViewConfig.PlayerView,
                Vector3.zero, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle()
                .WithArguments(horizontalBounds.position, verticalBounds.position);
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaucerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AsteroidParticleFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputAction>().AsSingle().NonLazy();
            Container.Bind<PlayerView>().FromInstance(playerViewInstance).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UiController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShootingManager>().AsSingle();
            Container.Bind<GameProfile>().AsSingle().NonLazy();
            var playerController = Container.InstantiateComponent<PlayerController>(playerViewInstance.gameObject);
            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStartsStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsManager>().AsSingle().WithArguments(playerViewInstance.transform).NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletShootHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RayShootHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesManager>().AsSingle().NonLazy();
            ZenjectInjectionSystem.UpdateContext(sceneContext);
        }
        
        private void InstantiatePools()
        {
            Container.BindMemoryPool<AsteroidFacade, AsteroidFacade.AsteroidPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<AsteroidView>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<SaucerFacade, SaucerFacade.SaucerPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<SaucerView>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<AsteroidParticleFacade, AsteroidParticleFacade.AsteroidParticlePool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<AsteroidParticleView>())
                .UnderTransformGroup("Enemies");
            

            Container.BindMemoryPool<BulletFacade, BulletFacade.BulletPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetWeaponView<BulletView>())
                .UnderTransformGroup("Сharges");
            
            Container.BindMemoryPool<RayFacade, RayFacade.RayPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetWeaponView<RayView>())
                .UnderTransformGroup("Сharges");
        }
    }
}