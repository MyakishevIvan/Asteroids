using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Player;
using Asteroids.Player.ShootSystem;
using BaseLogic.Controllers;
using UnityEngine;
using Zenject;
using Asteroids.GameProfile;
using Asteroids.Player.Weapon;

namespace BaseLogic.Installers
{
    public class SceneInstaller : MonoInstaller
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
            Container.BindInterfacesAndSelfTo<PlayerShootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletShootCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RayShootCreator>().AsSingle().NonLazy();
            Container.Bind<GameProfile>().AsSingle().NonLazy();
            var playerController = Container.InstantiateComponent<PlayerFacade>(playerViewInstance.gameObject);
            Container.Bind<PlayerFacade>().FromInstance(playerController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsStorage>().AsSingle().WithArguments(playerViewInstance.transform).NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesManager>().AsSingle().NonLazy();
            ZenjectInjectionSystem.UpdateContext(sceneContext);
        }
        
        private void InstantiatePools()
        {
            Container.BindMemoryPool<AsteroidFacade, AsteroidFacade.AsteroidPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<AsteroidView>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<SaucerFacade, SaucerFacade.SaucerPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<SaucerView>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<AsteroidParticleFacade, AsteroidParticleFacade.AsteroidParticlePool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<AsteroidParticleView>())
                .UnderTransformGroup("Enemies");
            

            Container.BindMemoryPool<BulletWeapon, BulletWeapon.BulletPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.WeaponConfig.GetWeaponView(WeaponType.Bullet))
                .UnderTransformGroup("Bullets");
            
            Container.BindMemoryPool<RayWeapon, RayWeapon.RayPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.WeaponConfig.GetWeaponView(WeaponType.Ray))
                .UnderTransformGroup("Rays");
        }
    }
}