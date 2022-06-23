using Asteroids.Configs;
using Asteroids.Windows;
using UnityEngine;

namespace Asteroids.Player
{
    public class PlayerMovementSystem 
    {
        private Transform _horizontalBounds;
        private Transform _verticalBounds;
        private Vector2 _currentInput;
        private Vector2 _smoothInputVelocity;
        private Vector2 _oldPosition;
        private PlayerHudParams _hudParams;
        private PlayerConfig _playerConfig;
        private Transform _playerTransform;

        public PlayerMovementSystem(Transform transform)
        {
            _hudParams = PlayerHudParams.Instance;
            _playerConfig = BalanceStorage.instance.PlayerConfig;
            _oldPosition = transform.position;
            _playerTransform = transform;
        }

        public void InitBounds(Transform boundsHorizontal, Transform boundsVertical)
        {
            _horizontalBounds = boundsHorizontal;
            _verticalBounds = boundsVertical;
        }
        
        public void PlayerMovementUpdate()
        {
            var movementInput = new Vector2(Input.GetAxis("Horizontal") * _playerConfig.RotationSpeed,
                Input.GetAxis("Vertical") * _playerConfig.PlayerSpeed);

            _currentInput =
                Vector2.SmoothDamp(_currentInput, movementInput, ref _smoothInputVelocity, _playerConfig.SmoothInputSpeed);

            _playerTransform.Translate(Vector3.up * _currentInput.y * Time.deltaTime);
            _playerTransform.Rotate(0, 0, -_currentInput.x * Time.deltaTime);

            CheckBounds();
            SendStats();
            _oldPosition = _playerTransform.position;
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