using System;
using Asteroids.Configs;
using Asteroids.Player;
using Asteroids.Windows;
using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids.BaseLogic
{
    public class SceneInstaller : MonoBehaviour
    {
        [SerializeField] private Transform horizontalBounds;
        [SerializeField] private Transform verticalBounds;
        [SerializeField] private BalanceStorage _balanceStorage;
        private EnemySpawner _enemySpawner;
        private static bool isFirstPlay = true;

        private void Awake()
        {
            if (isFirstPlay)
                OpenPromptWindow();
            else
                InstantiateScene();
        }

        private void InstantiateScene()
        {
            var playerView = InstantiatePlayer();
            _enemySpawner = new EnemySpawner(playerView.transform);
            WindowsManager.Instance.Open<Hud, WindowSetup.Empty>(null);
        }

        private PlayerView InstantiatePlayer()
        {
            var playerView = Instantiate(_balanceStorage.ObjectViewConfig.PlayerView);
            var playerController = playerView.gameObject.AddComponent<PlayerController>();
            playerController.AddBoundsToMovementSystem(horizontalBounds,verticalBounds);

            return playerView;
        }

        private void OpenPromptWindow()
        {
            WindowsManager.Instance.Open<PromptWindow, PromptWindowSetup>(new PromptWindowSetup()
            {
                onOkButtonClick = () =>
                {
                    InstantiateScene();
                    WindowsManager.Instance.Close<PromptWindow>();
                    isFirstPlay = false;
                },
                promptText = "Hold the lef mouse button to shoot bullets\nHold the right mouse button to shoot ray"
            });
        }

        private void OnDestroy()
        {
            _enemySpawner.UnsubscribeEvents();
        }
    }
}