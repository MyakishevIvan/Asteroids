using UnityEngine;

namespace Asteroids.Signals
{
    public class AsteroidBlowSignal
    {
        public readonly Transform AsteroidTransform;

        public AsteroidBlowSignal(Transform asteroidTransform)
        {
            AsteroidTransform = asteroidTransform;
        }
        
    }
}