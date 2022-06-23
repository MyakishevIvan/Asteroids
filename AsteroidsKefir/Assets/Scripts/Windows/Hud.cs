using System.Collections;
using TMPro;
using UnityEngine;

namespace Asteroids.Windows
{
    public class Hud : BaseWindow<WindowSetup.Empty>
    {
        [SerializeField] private TMP_Text _textStats;
        private Coroutine _coroutine;

        public override void Setup(WindowSetup.Empty setup)
        {
            _coroutine =  StartCoroutine(UpdateStats());
        }

        private IEnumerator UpdateStats()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                _textStats.text = PlayerHudParams.Instance.ToString();
            }
        }
        
        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }
    }

   
}