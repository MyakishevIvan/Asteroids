using System;
using System.Text;
using Asteroids.Configs;
using UnityEngine;
using Zenject;

namespace Player.Stats
{
    public class PlayerStartsStorage : IInitializable
    {
        [Inject] private BalanceStorage _balanceStorage;
        private int _score;
        private int _rayReloadTime;
        private int _rayCount;
        private float _speed;
        private StringBuilder _stringBuilder;
        
        public float Angel { get; set; }
        public Vector2 Coordinates { get; set; }
        
        public int RayCount
        {
            get => _rayCount;
            set
            {
                if (value < 0)
                    throw new Exception("Ray count can't be less than zero");
                _rayCount = value;
            }
        }
        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0)
                    throw new Exception("Speed can't be less than zero");
                _speed = value;
            }
        }
        public int RayReloadTime
        {
            get => _rayReloadTime;
            set
            {
                if (value < 0)
                    throw new Exception("Ray reload time can't be less than zero");
                _rayReloadTime = value;
            }
        }
        public int Score
        {
            get => _score;
            set
            {
                if (value < 0)
                    throw new Exception("Score can't be less than zero");
                _score = value;
            }
        }
        
        public void Initialize()
        {
            _stringBuilder = new StringBuilder();
        }
        
        public override string ToString()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append($"Score {Score}\n");
            _stringBuilder.Append($"Angel {Angel}\n");
            _stringBuilder.Append($"Speed {Speed}\n");
            _stringBuilder.Append($"Coordinates {Coordinates}\n");
            _stringBuilder.Append($"Ray Count {RayCount}\n");
            _stringBuilder.Append($"Ray Reload Time {RayReloadTime}");

            return _stringBuilder.ToString();
        }

        public void ResetStats()
        {
            Score = 0;
            Angel = 0;
            Coordinates = Vector2.zero;
            RayCount = _balanceStorage.WeaponConfig.RayShootCount;
            RayReloadTime = 0;
        }
    }
}