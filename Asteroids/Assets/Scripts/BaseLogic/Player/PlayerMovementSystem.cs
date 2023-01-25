using Asteroids.Boundary;
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
        [Inject] private LevelBoundary _levelBoundary;
        
        private Vector2 _currentInput;
        private Vector2 _smoothInputVelocity;

        private Transform PlayerTransform => _playerView.transform;
        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;
        
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
            if (PlayerTransform.position.x > _levelBoundary.Right)
                PlayerTransform.position = new Vector3(_levelBoundary.Left, PlayerTransform.position.y,
                    PlayerTransform.position.z);
            else if (PlayerTransform.position.x < _levelBoundary.Left)
                PlayerTransform.position =
                    new Vector3(_levelBoundary.Right, PlayerTransform.position.y, PlayerTransform.position.z);
        }

        private void CheckVerticalBounds()
        {
            if (PlayerTransform.position.y > _levelBoundary.Top)
                PlayerTransform.position =
                    new Vector3(PlayerTransform.position.x, _levelBoundary.Bottom, PlayerTransform.position.z);
            else if (PlayerTransform.position.y < _levelBoundary.Bottom)
                PlayerTransform.position =
                    new Vector3(PlayerTransform.position.x, _levelBoundary.Top, PlayerTransform.position.z);
        }
    }
}