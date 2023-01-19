using Asteroids.Enemies;
using Asteroids.GameProfile;
using Asteroids.Helper;
using Player.Stats;
using Player.ShootSystem;
using Asteroids.Signals;
using Player.Controller;
using UnityEngine;
using Zenject;

namespace BaseLogic.Controllers
{
    public class GameStateController : IInitializable, ITickable
    {
        [Inject] private UiController _uiController;
        [Inject] private GameProfile _gameProfile;
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerController _playerController;
        [Inject] private ShootingManager _shootingManager;
        [Inject] private EnemiesManager _enemiesManager;

        public void Initialize()
        {
            _signalBus.Subscribe<EndGameSignal>(EndGame);
            _uiController.OpenHud();

            if (!_gameProfile.IsUserPlayedBefore())
                _uiController.OpenFirstGamePrompt(StartGame);
            else
                StartGame();

            _gameProfile.SaveFirstGameStart();
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
            CoroutinesManager.StopAllRoutines();
            _enemiesManager.StopSpawnAndClearEnemies();
            _uiController.OpenLoseGamePrompt(StartGame);
            _shootingManager.UnSubscribeShootEvents();
            _shootingManager.DespawnAllShotWeapon();
            _playerController.DisablePlayer();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _gameProfile.RemoveSave();
            }
        }
    }
}