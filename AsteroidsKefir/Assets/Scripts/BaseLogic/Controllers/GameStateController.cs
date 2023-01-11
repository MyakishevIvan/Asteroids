using Asteroids.GameProfile;
using Asteroids.Helper;
using Asteroids.Player;
using Asteroids.Player.ShootSystem;
using Asteroids.Signals;
using Asteroids.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BaseLogic.Controllers
{
    public class GameStateController : IInitializable, ITickable
    {
        [Inject] private UiController _uiController;
        [Inject] private PlayerHudParamsCounter _playerHudParamsCounter;
        [Inject] private EnemiesSpawner _enemiesSpawner;
        [Inject] private GameProfile _gameProfile;
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerController _playerController;
        [Inject] private PlayerShootSystem _playerShootSystem;

        public void Initialize()
        {
            _signalBus.TryUnsubscribe<EndGameSignal>(EndGame);
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
            _enemiesSpawner.StartSpawnEnemies();
            _playerController.EnablePlayerView();
            _playerShootSystem.SubscribeShootEvents();
        }

        private void EndGame()
        {
            CoroutinesManager.StopAllRoutines();
            _enemiesSpawner.RemoveEnemies();
            _playerHudParamsCounter.StopCountParams();
            _uiController.OpenLoseGamePrompt(StartGame);
            _playerShootSystem.UnSubscribeShootEvents();
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