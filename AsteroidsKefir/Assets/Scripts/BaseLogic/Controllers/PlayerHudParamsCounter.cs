using System.Collections;
using System.Linq;
using System.Reflection;
using Asteroids.Helper;
using Asteroids.Signals;
using Asteroids.Player;
using UnityEngine;
using Zenject;

namespace Asteroids.Windows
{
    public class PlayerHudParamsCounter : IInitializable
    {
        [Inject] private PlayerView _playerView;
        [Inject] private SignalBus _signalBus;
        
        public float angel;
        public float speed;
        public int rayReloadTime;
        public Vector2 coordinates;
        private FieldInfo[] _fields;
        private PropertyInfo[] _properties;
        private Vector2 _oldPosition;
        private Coroutine _paramsCalculationRoutine;

        public int RayCount { get;  set; }
        public int Score { get; set; }
        
        public void Initialize()
        {
            _signalBus.TryUnsubscribe<AsteroidBlowSignal>(IncreaseScore);
            _signalBus.TryUnsubscribe<StartGameSignal>(StartCountParams);
            
            _signalBus.Subscribe<AsteroidBlowSignal>(IncreaseScore);
            _signalBus.Subscribe<StartGameSignal>(StartCountParams);
            
            var type = GetType();
           _fields = type.GetFields().Where(x => x.IsPublic).ToArray();
           _properties = type.GetProperties();
        }

        public void StopCountParams()
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
            rayReloadTime = 0;
        }
        
        private void StartCountParams(StartGameSignal signal)
        {
            RayCount = 3;
            _paramsCalculationRoutine = CoroutinesManager.StartRoutine(ParamsCalculation());
        }

        private IEnumerator ParamsCalculation()
        {
            while (true)
            {
                CalculateStats();
                _oldPosition = _playerView.transform.position;
                yield return new WaitForSeconds(0.3f);
            }
        }
        
        private void IncreaseScore(AsteroidBlowSignal signal) => Score++;
        
        public override string ToString()
        {
            var result = string.Empty;
            
            foreach (var properties  in _properties)
            {
                result += $"\n {properties.Name} - {properties.GetValue(this)}";
            }
            
            foreach (var field  in _fields)
            {
                result += $"\n {field.Name} - {field.GetValue(this)}";
            }
            
            return result;
        }
        
        private void CalculateStats()
        {
            coordinates = _playerView.transform.position;
            angel = _playerView.transform.rotation.eulerAngles.z;
            speed = Vector2.Distance(_playerView.transform.position, _oldPosition) / Time.deltaTime;
        }
    }
}