using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Configs/BalanceStorage")]
    
    public class BalanceStorage : ScriptableObject, IInitializable
    {
        [SerializeField] private ObjectViewConfig objectViewConfig;
        [SerializeField] private WeaponConfig weaponConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private TextConfig textConfig;

        private void Awake()
        {
            Debug.LogError("awake");
        }

        public  ObjectViewConfig ObjectViewConfig => objectViewConfig;
        public  WeaponConfig WeaponConfig => weaponConfig;
        public EnemiesConfig EnemiesConfig => enemiesConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public TextConfig TextConfig => textConfig;
        public void Initialize()
        {
            textConfig.OnCreat();
        }
    }
}
