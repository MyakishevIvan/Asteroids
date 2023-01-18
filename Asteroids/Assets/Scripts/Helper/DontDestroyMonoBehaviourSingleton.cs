using UnityEngine;

namespace Asteroids.Helper
{
    public class DontDestroyMonoBehaviourSingleton<T> :  MonoBehaviourSingleton<T> 
        where T : DontDestroyMonoBehaviourSingleton<T>
    {
        public new static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

        protected override void Init()
        {
        }
    }
    
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour 
        where T: MonoBehaviourSingleton<T>

    {
        protected static T _instance;

        public static T Instance => _instance;

        private void Awake()
        {
            T[] managers = GameObject.FindObjectsOfType<T>();
            
            if (managers.Length > 0)
                _instance = managers[0];
            else
                _instance = (T) this;

            Init();
        }

        protected abstract void Init();
    }
}