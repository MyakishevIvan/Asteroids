using Asteroids.Configs;
using Asteroids.Windows;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerMovementSystem : IInitializable
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerHudParams _hudParams;
        [Inject] private PlayerView _playerView;
        private Transform _horizontalBounds;
        private Transform _verticalBounds;
        private Vector2 _currentInput;
        private Vector2 _smoothInputVelocity;
        private Vector2 _oldPosition;
        
        private Transform _playerTransform => _playerView.transform;
        private PlayerConfig _playerConfig => _balanceStorage.PlayerConfig;

        public PlayerMovementSystem(Transform horizontalBounds, Transform verticalBounds)
        {
            _horizontalBounds = horizontalBounds;
            _verticalBounds = verticalBounds;
        }
        
        public void Initialize()
        {
            _oldPosition = _playerView.transform.position;
        }
        
        public void PlayerMovementUpdate()
        {
            var movementInput = new Vector2(Input.GetAxis("Horizontal") * _playerConfig.RotationSpeed,
                Input.GetAxis("Vertical") * _playerConfig.PlayerSpeed);

            _currentInput =
                Vector2.SmoothDamp(_currentInput, movementInput, ref _smoothInputVelocity,
                    _playerConfig.SmoothInputSpeed);

            _playerView.transform.Translate(Vector3.up * _currentInput.y * Time.deltaTime);
            _playerView.transform.Rotate(0, 0, -_currentInput.x * Time.deltaTime);

            CheckBounds();
            SendStats();
            
            _oldPosition = _playerView.transform.position;
        }

        private void SendStats()
        {
            _hudParams.coordinates = _playerTransform.position;
            _hudParams.angel = _playerTransform.rotation.eulerAngles.z;
            _hudParams.speed = Vector2.Distance(_playerTransform.position, _oldPosition) / Time.deltaTime;
        }

        private void CheckBounds()
        {
            if (_playerTransform.position.x > _horizontalBounds.position.x)
                _playerTransform.position = new Vector3(-_horizontalBounds.position.x, _playerTransform.position.y,
                    _playerTransform.position.z);
            else if (_playerTransform.position.x < -_horizontalBounds.position.x)
                _playerTransform.position =
                    new Vector3(_horizontalBounds.position.x, _playerTransform.position.y, _playerTransform.position.z);

            if (_playerTransform.position.y > _verticalBounds.position.y)
                _playerTransform.position =
                    new Vector3(_playerTransform.position.x, -_verticalBounds.position.y, _playerTransform.position.z);
            else if (_playerTransform.position.y < -_verticalBounds.position.y)
                _playerTransform.position =
                    new Vector3(_playerTransform.position.x, _verticalBounds.position.y, _playerTransform.position.z);
        }
        
    }
}