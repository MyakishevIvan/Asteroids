using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerMovementSystem _playerMovement;
        [Inject] private SignalBus _signalBus;
        
        private void Update()
        {
            _playerMovement.PlayerMovementUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(TextNameHelper.ENEMY))
            {
                _signalBus.Fire(new EndGameSignal());
                Destroy(gameObject);
            }
        }
    }
}