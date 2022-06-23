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
            var window = Instantiate((TWindow)original, transform);
            
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
    }
}