using System;
using Asteroids.Windows;
using Zenject;

namespace BaseLogic.Controllers
{
    public class UiController
    {
        [Inject] private WindowsManager _windowsManager;
        [Inject] private PlayerHudParamsCounter _playerHudParamsCounter;
        
        public void OpenHud()
        {
            _windowsManager.Open<Hud, HudSetup>(new HudSetup()
            {
                GetPlayerParams = () => _playerHudParamsCounter.ToString()
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
                promptText = "Hold the lef mouse button to shoot bullets\nHold the right mouse button to shoot ray"
            });
        }

        public void OpenLoseGamePrompt(Action endGameAction)
        {
            var setup = new PromptWindowSetup()
            {
                promptText = "You died\nScore " + _playerHudParamsCounter.Score,
                onOkButtonClick = () =>
                {
                    _windowsManager.Close<PromptWindow>();
                    _windowsManager.Close<Hud>();
                    endGameAction();
                }
            };

            WindowsManager.Instance.Open<PromptWindow, PromptWindowSetup>(setup);
        }
    }
}