using UnityEditor;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Configs/BalanceStorage")]
    
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private ObjectViewConfig objectViewConfig;
        [SerializeField] private WeaponConfig weaponConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        [SerializeField] private PlayerConfig playerConfig;

        public  ObjectViewConfig ObjectViewConfig => objectViewConfig;
        public  WeaponConfig WeaponConfig => weaponConfig;
        public EnemiesConfig EnemiesConfig => enemiesConfig;
        public PlayerConfig PlayerConfig => playerConfig;
    }
}
