using System;
using Asteroids.Configs;
using Zenject;

namespace Asteroids.Enemies
{
    public class EnemiesSettingsContainer : IInitializable
    {
        [Inject] private BalanceStorage _balanceStorage;
        private TrajectorySettings[] _cashedTrajectorySettings;
        private AsteroidParticleTrajectorySettings[] _cashedAsteroidParticleTrajectorySettings;
        private int _cashedSettingsIndex;
        private int _cashedAsteroidParticleSettingsIndex;
        
        private int CashedSettingsIndex
        {
            get => _cashedSettingsIndex;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _cashedSettingsIndex = value == _balanceStorage.EnemiesConfig.CashedEnemiesSettingsCount ? 0 : value;
            }
        }
        private int CashedAsteroidParticleSettingsIndex
        {
            get => _cashedAsteroidParticleSettingsIndex;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _cashedAsteroidParticleSettingsIndex = value == _balanceStorage.EnemiesConfig.CashedAsteroidParticleSettingsCount ? 0 : value;
            }
        }
        
        public TrajectorySettings GetTrajectorySettings() => _cashedTrajectorySettings[CashedSettingsIndex++];
        public  AsteroidParticleTrajectorySettings GetAsteroidTrajectorySettings() => _cashedAsteroidParticleTrajectorySettings[CashedAsteroidParticleSettingsIndex++];
        
        public void Initialize()
        {
            CashedSettingsIndex = 0;
            CashedAsteroidParticleSettingsIndex = 0; 
        }
        
        public void CreatCashedEnemiesSettings()
        {
            CreatSettingsForIntegerEnemies();
            CreatSettingsForParticleEnemies();
        }

        private void CreatSettingsForIntegerEnemies()
        {
            var spawnRadius = _balanceStorage.EnemiesConfig.EnemySpawnRadius;
            var trajectoryVariance = _balanceStorage.EnemiesConfig.TrajectoryVariance;
            _cashedTrajectorySettings = new TrajectorySettings[_balanceStorage.EnemiesConfig.CashedEnemiesSettingsCount];

            for (var i = 0; i < _balanceStorage.EnemiesConfig.CashedEnemiesSettingsCount; i++)
                _cashedTrajectorySettings[i] = new TrajectorySettings(spawnRadius, trajectoryVariance);
        }

        private void CreatSettingsForParticleEnemies()
        {
            _cashedAsteroidParticleTrajectorySettings =
                new AsteroidParticleTrajectorySettings[_balanceStorage.EnemiesConfig.CashedAsteroidParticleSettingsCount];

            for (var i = 0; i < _balanceStorage.EnemiesConfig.CashedAsteroidParticleSettingsCount; i++)
                _cashedAsteroidParticleTrajectorySettings[i] = new AsteroidParticleTrajectorySettings();
        }
    }
}