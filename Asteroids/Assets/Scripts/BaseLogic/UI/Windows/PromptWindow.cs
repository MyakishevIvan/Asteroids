using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Asteroids.UI.Windows
{
    public class PromptWindow : BaseWindow<PromptWindowSetup>
    {
        [SerializeField] private Button okButton;
        [SerializeField] private TMP_Text promptText;

        public override void Setup(PromptWindowSetup setup)
        {
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(()=> setup.onOkButtonClick());
            promptText.text = setup.promptText;
        }
    }

    public class PromptWindowSetup : WindowSetup
    {
        public Action onOkButtonClick;
        public string promptText;
    }
}