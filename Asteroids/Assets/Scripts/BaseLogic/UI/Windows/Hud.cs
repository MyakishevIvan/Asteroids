using System;
using System.Collections;
using Asteroids.UI.Elements;
using TMPro;
using UnityEngine;

namespace Asteroids.UI.Windows
{
    public class Hud : BaseWindow<HudSetup>
    {
        [SerializeField] private TMP_Text textStats;
        [SerializeField] private Switcher switcher; 
        private Coroutine _coroutine;
        private HudSetup _hudSetup;
        
        public override void Setup(HudSetup setup)
        {
            _hudSetup = setup;
            _coroutine = StartCoroutine(UpdateStats());
            switcher.Init(setup.soundIsOn, setup.onSoundAction, setup.offSoundAction);
        }
        
        private IEnumerator UpdateStats()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                textStats.text = _hudSetup.GetPlayerParams();
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }
    }

    public class HudSetup : WindowSetup
    {
        public Func<string> GetPlayerParams;
        public Action onSoundAction;
        public Action offSoundAction;
        public bool soundIsOn;
    }

}