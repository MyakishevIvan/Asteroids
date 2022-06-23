using Asteroids.Helper;
using Asteroids.Player.ShootSystem;
using Asteroids.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerShootSystem _playerShootSystem;
        private PlayerMovementSystem _playerMovement;

        private void Awake()
        {
            _playerShootSystem = new PlayerShootSystem(transform);
            _playerMovement = new PlayerMovementSystem(transform);
            PlayerHudParams.Instance.Score = 0;
        }

        public void AddBoundsToMovementSystem(Transform horizontalBounds, Transform verticalBounds)
        {
            _playerMovement.InitBounds(horizontalBounds, verticalBounds);
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
                    promptText = "You died\nScore " + PlayerHudParams.Instance.Score,
                    onOkButtonClick = () =>
                    {
                        WindowsManager.Instance.Close<PromptWindow>();
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