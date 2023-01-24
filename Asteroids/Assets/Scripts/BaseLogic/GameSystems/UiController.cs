using System;
using Asteroids.Audio;
using Asteroids.Configs;
using Asteroids.UI.Windows;
using Asteroids.WindowsManager;
using Player.Stats;
using Zenject;

namespace Game.Controllers
{
    public class UiController
    {
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private PlayerStatsManager _playerStatsManager;
        [Inject] private SoundsController _soundsController;
        
        public void OpenHud()
        {
            _windowsManager.Open<Hud, HudSetup>(new HudSetup()
            {
                GetPlayerParams = () => _playerStatsManager.ToString(),
                soundIsOn =  _balanceStorage.SoundsConfig.InitialVolumeState,
                onSoundAction =  () => _soundsController.ChangeSoundsState(true),
                offSoundAction =  () => _soundsController.ChangeSoundsState(false)
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