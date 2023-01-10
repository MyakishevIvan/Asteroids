using Asteroids.GameProfile;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine.SceneManagement;
using Zenject;

namespace BaseLogic.Controllers
{
    public class GameStateController : IInitializable
    {
        [Inject] private UiController _uiController;
        [Inject] private GameProfile _gameProfile;
        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.TryUnsubscribe<EndGameSignal>(EndGame);
            _signalBus.Subscribe<EndGameSignal>(EndGame);
            _uiController.OpenHud();

            if (!_gameProfile.IsUserPlayedBefore())
                _uiController.OpenFirstGamePrompt(StartGame);
            else
                StartGame();
        }

        private void StartGame()
        {
            _gameProfile.SaveFirstGameStart();
            _signalBus.Fire(new StartGameSignal());
        }

        private void EndGame()
        {
            CoroutinesManager.StopAllRoutines();
            _uiController.OpenLoseGamePrompt(() => SceneManager.LoadScene
                (
                    TextNameHelper.BASE_SCENE,
                    LoadSceneMode.Single
                )
            );
        }
    }
}