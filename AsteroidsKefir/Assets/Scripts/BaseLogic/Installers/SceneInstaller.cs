using Asteroids.Configs;
using Asteroids.Enemies;
using Asteroids.Enums;
using Asteroids.Player;
using Asteroids.Player.ShootSystem;
using Asteroids.Windows;
using BaseLogic.Controllers;
using UnityEngine;
using Zenject;
using Asteroids.GameProfile;

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
            var playerViewInstance = Container.InstantiatePrefabForComponent<PlayerView>(
                _balanceStorage.ObjectViewConfig.PlayerView,
                Vector3.zero, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<PlayerInputAction>().AsSingle().NonLazy();
            Container.Bind<PlayerView>().FromInstance(playerViewInstance).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudParamsCounter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UiController>().AsSingle().NonLazy();
            Container.Bind<EnemiesControlSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletShootCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RayShootCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle()
                .WithArguments(horizontalBounds.position, verticalBounds.position);
            InstantiatePools();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
            Container.Bind<GameProfile>().AsSingle().NonLazy();
            var playerController = Container.InstantiateComponent<PlayerController>(playerViewInstance.gameObject);
            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();
            ZenjectInjectionSystem.UpdateContext(sceneContext);
        }
        
        private void InstantiatePools()
        {
            Container.BindFactory<EnemyView, EnemyView.AsteroidFactory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<EnemyView, EnemyViewPool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView(EnemyType.Asteroid))
                    .UnderTransformGroup("Enemies"));
            
            Container.BindFactory<EnemyView, EnemyView.AsteroidParticleFactory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<EnemyView, EnemyViewPool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView(EnemyType.AsteroidParticle))
                    .UnderTransformGroup("Enemies"));
            
            Container.BindFactory<EnemyView, EnemyView.SaucerFactory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<EnemyView, EnemyViewPool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_balanceStorage.ObjectViewConfig.GetEnemyView(EnemyType.Saucer))
                    .UnderTransformGroup("Enemies"));
        }
    }

    // We could just use FromMonoPoolableMemoryPool above, but we have to use these instead
    // for IL2CPP to work
    class EnemyViewPool : MonoPoolableMemoryPool<IMemoryPool, EnemyView>
    {
    }
}