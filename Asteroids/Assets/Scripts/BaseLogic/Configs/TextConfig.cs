using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(TextConfig), menuName = "Configs/" +nameof(TextConfig))]
    public class TextConfig : ScriptableObject
    {
        [SerializeField] private string startGameText;
        [SerializeField] private string endGameText;
        
        public void OnCreat()
        {
            StartGameText = startGameText.Replace("NEWLINE","\n");
            EndGameText = endGameText.Replace("NEWLINE","\n");
        }
        
        public string StartGameText { get; private set; }
        public string EndGameText { get; private set; }
    }
}