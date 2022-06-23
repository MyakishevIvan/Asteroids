using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        public EnemyType EnemyType => enemyType;
    }
}