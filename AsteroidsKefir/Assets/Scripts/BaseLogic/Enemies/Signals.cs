using UnityEngine;

namespace Asteroids.Signals
{
    public class AsteroidDamageSignal
    {
        public readonly Transform AsteroidTransform;

        public AsteroidDamageSignal(Transform asteroidTransform)
        {
            AsteroidTransform = asteroidTransform;
        }
    }
}