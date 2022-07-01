using Asteroids.Helper;
using Asteroids.Player.ShootSystem;
using Asteroids.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerShootSystem _playerShootSystem;
        [Inject] private PlayerMovementSystem _playerMovement;
        [Inject] private PlayerHudParams _playerHudParams;

        private void Start()
        {
            _playerHudParams.Score = 0;
        }
        
        private void Update()
        {
            _playerShootSystem.ShootUpdate();
            _playerMovement.PlayerMovementUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var setup = new PromptWindowSetup()
                {
                    promptText = "You died\nScore " + _playerHudParams.Score,
                    onOkButtonClick = () =>
                    {
                        WindowsManager.Instance.Close<PromptWindow>();
                        WindowsManager.Instance.Close<Hud>();
                        SceneManager.LoadScene("BaseScene", LoadSceneMode.Single);
                    }
                };

                WindowsManager.Instance.Open<PromptWindow, PromptWindowSetup>(setup);
                CoroutinesManager.StopAllRoutines();
                Destroy(gameObject);
            }
        }
    }
}