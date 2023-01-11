using System.Collections;
using System.Linq;
using System.Reflection;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Player
{
    public class PlayerStatsStorage : IInitializable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BalanceStorage _balanceStorage;

        private Transform _playerTransform;
        public float angel;
        public float speed;
        public Vector2 coordinates;
        private FieldInfo[] _fields;
        private PropertyInfo[] _properties;
        private Vector2 _oldPosition;
        private Coroutine _paramsCalculationRoutine;

        public int RayReloadTime { get; set; }
        public int RayCount { get; set; }
        public int Score { get; private set; }

        public PlayerStatsStorage(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            var type = GetType();
            _fields = type.GetFields().Where(x => x.IsPublic).ToArray();
            _properties = type.GetProperties();
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<RayEndedSignal>(SetReloadTime);
            _signalBus.Subscribe<RayReloadTimeEned>(ReloadRay);
            _signalBus.Subscribe<AsteroidBlowSignal>(IncreaseScore);
            _signalBus.Subscribe<StartGameSignal>(StartCountParams);
            _signalBus.Subscribe<EndGameSignal>(StopCountParams);
        }
        
        private void SetReloadTime() => RayReloadTime = _balanceStorage.WeaponConfig.ReloadTime;
        private void ReloadRay() => RayCount = _balanceStorage.WeaponConfig.RayShootCount;
        private void IncreaseScore(AsteroidBlowSignal signal) => Score++;

        private void StartCountParams(StartGameSignal signal)
        {
            SetBaseStats();
            _paramsCalculationRoutine = CoroutinesManager.StartRoutine(ParamsCalculation());
        }
        
        private void SetBaseStats()
        {
            RayCount = _balanceStorage.WeaponConfig.RayShootCount;
            RayReloadTime = 0;
        }
        
        private IEnumerator ParamsCalculation()
        {
            while (true)
            {
                CalculateStats();
                _oldPosition = _playerTransform.position;
                yield return new WaitForSeconds(0.3f);
            }
        }

        private void CalculateStats()
        {
            coordinates = _playerTransform.position;
            angel = _playerTransform.rotation.eulerAngles.z;
            speed = Vector2.Distance(_playerTransform.position, _oldPosition) / Time.deltaTime;
        }

        private void StopCountParams()
        {
            CoroutinesManager.StopRoutine(_paramsCalculationRoutine);
            ParamsReset();
        }

        private void ParamsReset()
        {
            speed = 0;
            angel = 0;
            Score = 0;
            coordinates = Vector2.zero;
            RayReloadTime = 0;
        }
        
        public override string ToString()
        {
            var result = string.Empty;

            foreach (var properties in _properties)
            {
                result += $"\n {properties.Name} - {properties.GetValue(this)}";
            }
            
            foreach (var field in _fields)
            {
                result += $"\n {field.Name} - {field.GetValue(this)}";
            }

            return result;
        }
    }
}