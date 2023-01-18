using UnityEngine;

namespace Asteroids.Windows
{
    public abstract class BaseWindow<TSetup> : Window where TSetup : WindowSetup
    {
        public abstract void Setup(TSetup setup);
    }
    
    public abstract class WindowSetup
    {
        public sealed class Empty : WindowSetup
        {
            public static readonly Empty Instance = new Empty();
        }
    }
    
    public class Window : MonoBehaviour{}
}