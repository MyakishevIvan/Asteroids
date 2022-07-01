using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Asteroids.Windows
{
    public class Hud : BaseWindow<WindowSetup.Empty>
    {
        [SerializeField] private TMP_Text _textStats;
        [Inject] private PlayerHudParams _playerHudParams;
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
                _textStats.text = _playerHudParams.ToString();
            }
        }
        
        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }
    }

   
}