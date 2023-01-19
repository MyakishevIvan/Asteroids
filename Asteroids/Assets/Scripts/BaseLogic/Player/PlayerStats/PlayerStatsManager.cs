using System.Collections;
using Asteroids.Configs;
using Asteroids.Helper;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Player.Stats
{
    public class PlayerStatsManager : IInitializable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private PlayerStartsStorage _playerStatsStorage;

        private Transform _playerTransform;
        private Vector2 _oldPosition;
        private Coroutine _paramsCalculationRoutine;
        private Coroutine _reloadRoutine;
        public int Score => _playerStatsStorage.Score;

        public PlayerStatsManager(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AsteroidBlowSignal>(IncreaseScore);
            _signalBus.Subscribe<StartGameSignal>(StartCountParams);
            _signalBus.Subscribe<EndGameSignal>(StopCountParams);
        }

        private void IncreaseScore(AsteroidBlowSignal signal) => _playerStatsStorage.Score++;

        private void StartCountParams(StartGameSignal signal)
        {
            SetBaseStats();
            _paramsCalculationRoutine = CoroutinesManager.StartRoutine(ParamsCalculation());
        }

        private void SetBaseStats() => _playerStatsStorage.ResetStats();
        private void StopCountParams() => CoroutinesManager.StopRoutine(_paramsCalculationRoutine);


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
            _playerStatsStorage.Coordinates = _playerTransform.position;
            _playerStatsStorage.Angel = _playerTransform.rotation.eulerAngles.z;
            _playerStatsStorage.Speed = Vector2.Distance(_playerTransform.position, _oldPosition) / Time.deltaTime;
        }

        public bool TrySpendRays()
        {
            var isReloadTimeIsEnded = _playerStatsStorage.RayReloadTime == 0;
            
            if (isReloadTimeIsEnded)
            {
                _playerStatsStorage.RayCount--;
                CheckRaysCountAfterSpending();
            }
            
            return isReloadTimeIsEnded;
        }
        
        private void CheckRaysCountAfterSpending()
        {
            if (IsRaysEnded())
            {
                _playerStatsStorage.RayReloadTime = _balanceStorage.WeaponConfig.ReloadTime;
                _reloadRoutine = CoroutinesManager.StartRoutine(SpendReloadTime());
            }
        }

        private bool IsRaysEnded() => _playerStatsStorage.RayCount == 0;

        private IEnumerator SpendReloadTime()
        {
            while (_playerStatsStorage.RayReloadTime != 0)
            {
                yield return new WaitForSeconds(1);
                _playerStatsStorage.RayReloadTime--;
            }

            _playerStatsStorage.RayCount = _balanceStorage.WeaponConfig.RayShootCount;
            CoroutinesManager.StopRoutine(_reloadRoutine);
        }

        public override string ToString() => _playerStatsStorage.ToString();
    }
}