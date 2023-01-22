using System;
using Asteroids.Configs;
using Player.Stats;
using Asteroids.Windows;
using Zenject;

namespace Game.Controllers
{
    public class UiController
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private PlayerStatsManager _playerStatsManager;
        
        public void OpenHud()
        {
            _windowsManager.Open<Hud, HudSetup>(new HudSetup()
            {
                GetPlayerParams = () => _playerStatsManager.ToString()
            });
        }
        
        public void OpenFirstGamePrompt(Action startGameAction)
        {
            var text = _balanceStorage.TextConfig.StartGameText;
            OpenPopUp(text, startGameAction);
        }

        public void OpenLoseGamePrompt(Action endGameAction)
        {
            var text = _balanceStorage.TextConfig.EndGameText + _playerStatsManager.Score;
            OpenPopUp(text, endGameAction);
        }

        private void OpenPopUp(string text, Action onButtonClickActoin)
        {
            var setup = new PromptWindowSetup()
            {
                promptText = text,
                onOkButtonClick = () =>
                {
                    _windowsManager.Close<PromptWindow>();
                    onButtonClickActoin();
                }
            };

            WindowsManager.Instance.Open<PromptWindow, PromptWindowSetup>(setup);
        }
    }
}