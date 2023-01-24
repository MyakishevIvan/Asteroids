using Asteroids.Enemies;
using Asteroids.GameProfile;
using Player.ShootSystem;
using Asteroids.Signals;
using Player.Controller;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class GameStateFacade : IInitializable, ITickable
    {
        [Inject] private UiController _uiController;
        [Inject] private GameProfileSettings _gameProfileSettings;
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerController _playerController;
        [Inject] private ShootingManager _shootingManager;
        [Inject] private EnemiesManager _enemiesManager;

        public void Initialize()
        {
            _signalBus.Subscribe<EndGameSignal>(EndGame);
            _uiController.OpenHud();

            if (!_gameProfileSettings.IsUserPlayedBefore())
                _uiController.OpenFirstGamePrompt(StartGame);
            else
                StartGame();

            _gameProfileSettings.SaveFirstGameStart();
        }

        private void StartGame()
        {
            _signalBus.Fire(new StartGameSignal());
            _enemiesManager.StartSpawnEnemies();
            _playerController.EnablePlayerView();
            _shootingManager.SubscribeShootEvents();
        }

        private void EndGame()
        {
            _enemiesManager.StopSpawnAndDespawnEnemies();
            _uiController.OpenLoseGamePrompt(StartGame);
            _shootingManager.UnSubscribeShootEvents();
            _shootingManager.DespawnAllShotWeapon();
            _playerController.DisablePlayer();
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
                _gameProfileSettings.RemoveSave();
        }
    }
}