using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/"+nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float playerSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float smoothInputSpeed;

        public float PlayerSpeed => playerSpeed;
        public float RotationSpeed => rotationSpeed;
        public float SmoothInputSpeed => smoothInputSpeed;
    }
}