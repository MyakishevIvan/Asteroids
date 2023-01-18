using System;
using Asteroids.Player;
using Asteroids.Windows;
using Zenject;

namespace BaseLogic.Controllers
{
    public class UiController
    {
        [Inject] private WindowsManager _windowsManager;
        [Inject] private PlayerStatsStorage _playerStatsStorage;
        
        public void OpenHud()
        {
            _windowsManager.Open<Hud, HudSetup>(new HudSetup()
            {
                GetPlayerParams = () => _playerStatsStorage.ToString()
            });
        }
        
        public void OpenFirstGamePrompt(Action startGameAction)
        {
            _windowsManager.Open<PromptWindow, PromptWindowSetup>(new PromptWindowSetup()
            {
                onOkButtonClick = () =>
                {
                    _windowsManager.Close<PromptWindow>();
                    startGameAction();
                },
                promptText = "Hold the lef mouse button to shoot bullets" +
                             "\nHold the right mouse button to shoot ray" +
                             "\nUse WASD FOR CONTROL SHIP and don't forget." +
                             "\nRays kill everything but has reload time!"
            });
        }

        public void OpenLoseGamePrompt(Action endGameAction)
        {
            var setup = new PromptWindowSetup()
            {
                promptText = "You died\nScore for destroyed big asteroids " + _playerStatsStorage.Score,
                onOkButtonClick = () =>
                {
                    _windowsManager.Close<PromptWindow>();
                    endGameAction();
                }
            };

            WindowsManager.Instance.Open<PromptWindow, PromptWindowSetup>(setup);
        }
    }
}