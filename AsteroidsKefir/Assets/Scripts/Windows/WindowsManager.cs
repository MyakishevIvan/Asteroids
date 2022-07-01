using System.Collections.Generic;
using Asteroids.Helper;
using UnityEngine;
using System;

namespace Asteroids.Windows
{
    public class WindowsManager : DontDestroyMonoBehaviourSingleton<WindowsManager>
    {
        [SerializeField] private List<Window> windowPrefabs;
        private readonly Dictionary<Type, Window> _windowPrefabsByType = new();
        private readonly Dictionary<Type, Window> _currentwindows = new();

        public delegate Window InstantiateWindowDelegate(Window window);

        public static InstantiateWindowDelegate CustomWindowInstantiator;

        private void AddWindowPrefabs(List<Window> prefabs)
        {
            foreach (var windowPrefab in prefabs)
            {
                _windowPrefabsByType[windowPrefab.GetType()] = windowPrefab;
            }
        }

        public void Open<TWindow, TSetup>(TSetup setup)
            where TWindow : BaseWindow<TSetup>, new()
            where TSetup : WindowSetup, new()
        {
            if (_windowPrefabsByType.Count == 0)
                AddWindowPrefabs(windowPrefabs);

            var windowType = typeof(TWindow);

            if (!_windowPrefabsByType.ContainsKey(windowType))
                throw new Exception($"No prefab for window {windowType}");

            var original = _windowPrefabsByType[windowType];
            var window = InstantiateWindow((TWindow)original);
            
            _currentwindows.Add(windowType, window);
            window.Setup(setup);
        }

        public void Close<TWindow>() where TWindow : Window
        {
            var windowType = typeof(TWindow);

            if (!_currentwindows.TryGetValue(windowType, out var window))
                return;

            _currentwindows.Remove(windowType);
            Destroy(window.gameObject);
        }
        
        private BaseWindow<TSetup> InstantiateWindow<TSetup>(BaseWindow<TSetup> original) where TSetup : WindowSetup, new()
        {
            var window = CustomWindowInstantiator != null
                ? (BaseWindow<TSetup>) CustomWindowInstantiator(original)
                : Instantiate(original, transform);

            return window;
        }
    }
}