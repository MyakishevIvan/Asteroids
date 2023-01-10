using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Asteroids.Windows
{
    public class Hud : BaseWindow<HudSetup>
    {
        [SerializeField] private TMP_Text _textStats;
        private Coroutine _coroutine;
        private HudSetup _hudSetup;
        
        public override void Setup(HudSetup setup)
        {
            _hudSetup = setup;
            _coroutine = StartCoroutine(UpdateStats());
        }
        
        private IEnumerator UpdateStats()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                _textStats.text = _hudSetup.GetPlayerParams();
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
    }

}