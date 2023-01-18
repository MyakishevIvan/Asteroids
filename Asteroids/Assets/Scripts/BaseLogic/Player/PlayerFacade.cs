using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [Inject] private PlayerMovementSystem _playerMovement;
        [Inject] private SignalBus _signalBus;

        private void Awake()
        {
            DisablePlayer();
        }

        private void Update()
        {
            _playerMovement.PlayerMovementUpdate();
        }

        private void OnTriggerEnter2D(Collider2D enemy)
        {
            if (enemy.gameObject.layer == LayerMask.NameToLayer(TextNameHelper.ENEMY))
                _signalBus.Fire(new EndGameSignal());
        }
        
        public void DisablePlayer()
        {
            gameObject.SetActive(false);
        }
        
        public void EnablePlayerView()
        {
            Invoke(nameof(SetActivePlayer), 0.5f);
            gameObject.transform.position = Vector3.zero;
        }

        private void SetActivePlayer() => gameObject.SetActive(true);
    }
}