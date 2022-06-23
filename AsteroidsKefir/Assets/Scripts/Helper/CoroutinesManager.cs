using System.Collections;
using UnityEngine;

namespace Asteroids.Helper
{
    public sealed class CoroutinesManager : Singleton<CoroutinesManager>
    {
        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
          return  Instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
        
        public static void StopAllRoutines() => Instance.StopAllCoroutines();
    }
}