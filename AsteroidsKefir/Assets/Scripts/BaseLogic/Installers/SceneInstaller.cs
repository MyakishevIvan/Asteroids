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
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaucerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AsteroidParticleFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputAction>().AsSingle().NonLazy();
            Container.Bind<PlayerView>().FromInstance(playerViewInstance).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UiController>().AsSingle().NonLazy();
            Container.Bind<EnemiesControlSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletShootCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RayShootCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle()
                .WithArguments(horizontalBounds.position, verticalBounds.position);
            Container.Bind<GameProfile>().AsSingle().NonLazy();
            var playerController = Container.InstantiateComponent<PlayerController>(playerViewInstance.gameObject);
            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsStorage>().AsSingle().WithArguments(playerViewInstance.transform).NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesManager>().AsSingle().NonLazy();
            ZenjectInjectionSystem.UpdateContext(sceneContext);
        }
        
        private void InstantiatePools()
        {
            Container.BindMemoryPool<Asteroid, Asteroid.AsteroidPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<Asteroid>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<Saucer, Saucer.SaucerPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<Saucer>())
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<AsteroidParticle, AsteroidParticle.AsteroidPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemy<AsteroidParticle>())
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