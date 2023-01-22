using System.Collections;
using UnityEngine;

namespace Asteroids.Enemies
{
    public interface ITemporaryEnemy
    {
        public Coroutine DespawnRoutine { get; set; }
        public IEnumerator DespawnCoroutine();
        public void StartDespawnAfterEndLifeTimeRoutine();
        public void StopDespawnAfterLifeTimeRoutine();
    }
}