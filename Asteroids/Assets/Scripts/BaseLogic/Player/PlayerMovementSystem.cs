using Asteroids.Configs;
using Player.View;
using UnityEngine;
using Zenject;

namespace Player.MovementSystem
{
    public class PlayerMovementSystem : IInitializable
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerView _playerView;
        [Inject] private PlayerInputAction _playerInputAction;
        
        private Vector2 _horizontalBounds;
        private Vector2 _verticalBounds;
        private Vector2 _currentInput;
        private Vector2 _smoothInputVelocity;

        private Transform PlayerTransform => _playerView.transform;
        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;

        public PlayerMovementSystem(Vector3 horizontalBounds, Vector3 verticalBounds)
        {
            _horizontalBounds = horizontalBounds;
            _verticalBounds = verticalBounds;
        }

        public void Initialize()
        {
            _playerInputAction.Enable();
        }

        public void PlayerMovementUpdate()
        {
            CalculateVector();
            MovePlayer();
            CheckBounds();
        }

        private void CalculateVector()
        {
            var movementInput = _playerInputAction.Player.Movement.ReadValue<Vector2>();
            _currentInput =
                Vector2.SmoothDamp
                (
                    _currentInput,
                    new Vector2(movementInput.x * PlayerConfig.RotationSpeed,
                        movementInput.y * PlayerConfig.PlayerSpeed),
                    ref _smoothInputVelocity,
                    PlayerConfig.SmoothInputSpeed
                );
        }

        private void MovePlayer()
        {
            _playerView.transform.Translate(Vector3.up * _currentInput.y * Time.deltaTime);
            _playerView.transform.Rotate(0, 0, -_currentInput.x * Time.deltaTime);
        }
        
        private void CheckBounds()
        {
            CheckHorizontalBounds();
            CheckVerticalBounds();
        }
        
        private void CheckHorizontalBounds()
        {
            if (PlayerTransform.position.x > _horizontalBounds.x)
                PlayerTransform.position = new Vector3(-_horizontalBounds.x, PlayerTransform.position.y,
                    PlayerTransform.position.z);
            else if (PlayerTransform.position.x < -_horizontalBounds.x)
                PlayerTransform.position =
                    new Vector3(_horizontalBounds.x, PlayerTransform.position.y, PlayerTransform.position.z);
        }

        private void CheckVerticalBounds()
        {
            if (PlayerTransform.position.y > _verticalBounds.y)
                PlayerTransform.position =
                    new Vector3(PlayerTransform.position.x, -_verticalBounds.y, PlayerTransform.position.z);
            else if (PlayerTransform.position.y < -_verticalBounds.y)
                PlayerTransform.position =
                    new Vector3(PlayerTransform.position.x, _verticalBounds.y, PlayerTransform.position.z);
        }
    }
}