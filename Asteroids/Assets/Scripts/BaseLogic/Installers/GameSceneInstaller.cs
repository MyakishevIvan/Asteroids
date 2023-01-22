using Asteroids.Configs;
using Asteroids.Enemies;
using Player.Stats;
using UnityEngine;
using Zenject;
using Asteroids.GameProfile;
using Enemies.View;
using Game.Controllers;
using Player.Controller;
using Player.MovementSystem;
using Player.ShootSystem;
using Player.View;
using Weapons.View;

namespace Scene.Installer
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Inject] private BalanceStorage _balanceStorage;
        [SerializeField] private Transform horizontalBounds;
        [SerializeField] private Transform verticalBounds;
        private PlayerView _playerView;

        public override void InstallBindings()
        {
            InstantiatePools();
            BindPlayer();
            BindWeaponSystem();
            BindEnemies();
            BindGameSystems();
        }
        
        private void BindPlayer()
        {
            _playerView = Container.InstantiatePrefabForComponent<PlayerView>(
                _balanceStorage.ObjectViewConfig.PlayerView,
                Vector3.zero, Quaternion.identity, null);
            Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputAction>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle()
                .WithArguments(horizontalBounds.position, verticalBounds.position);
            var playerController = Container.InstantiateComponent<PlayerController>(_playerView.gameObject);
            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStartsStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsManager>().AsSingle().WithArguments(_playerView.transform).NonLazy();
        }

        private void BindWeaponSystem()
        {
            Container.BindInterfacesAndSelfTo<ShootingManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletShootHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RayShootHandler>().AsSingle().NonLazy();
        }
        
        private void BindEnemies()
        {
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaucerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AsteroidParticleFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesSettingsContainer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesManager>().AsSingle().NonLazy();
        }

        private void BindGameSystems()
        {
            Container.BindInterfacesAndSelfTo<UiController>().AsSingle().NonLazy();
            Container.Bind<GameProfile>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateFacade>().AsSingle().NonLazy();
        }
        
        private void InstantiatePools()
        {
            InstantiateEnemiesPools();
            InstantiateWeaponsPools();
            
        }
        private void InstantiateEnemiesPools()
        {
            Container.BindMemoryPool<AsteroidController, AsteroidController.AsteroidPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<AsteroidView>())
                .UnderTransformGroup("Enemies");

            Container.BindMemoryPool<SaucerController, SaucerController.SaucerPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<SaucerView>())
                .UnderTransformGroup("Enemies");

            Container.BindMemoryPool<AsteroidParticleController, AsteroidParticleController.AsteroidParticlePool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView<AsteroidParticleView>())
                .UnderTransformGroup("Enemies");
        }

        private void InstantiateWeaponsPools()
        {
            Container.BindMemoryPool<BulletController, BulletController.BulletPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetWeaponView<BulletView>())
                .UnderTransformGroup("Сharges");

            Container.BindMemoryPool<RayController, RayController.RayPool>()
                .WithInitialSize(5)
                .FromNewComponentOnNewPrefab(_balanceStorage.ObjectViewConfig.GetWeaponView<RayView>())
                .UnderTransformGroup("Сharges");
        }
    }
}