using System;
using Asteroids.Configs;
using Asteroids.Enums;
using Asteroids.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundsController : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BalanceStorage _balanceStorage;
        private SoundsConfig _soundsConfig;
        private AudioSource _audioSource;

        private void Awake()
        {
            SetSettings();
        }

        private void SetSettings()
        {
            _soundsConfig = _balanceStorage.SoundsConfig;
            _signalBus.Subscribe<StartGameSignal>(PlayBackgroundSound);
            _signalBus.Subscribe<EndGameSignal>(StopBackgroundSound);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.rolloffMode = AudioRolloffMode.Linear;
            _audioSource.spatialBlend = 1;
            _audioSource.mute = false;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
        }

        private void PlayBackgroundSound(StartGameSignal signal) => _audioSource.Play();
        private void StopBackgroundSound(EndGameSignal signal) => _audioSource.Stop();

        public void ChangeSoundsState(bool isOn) => _audioSource.volume = isOn ? _soundsConfig.Volume : 0f;

        public void PlaySound(SoundType soundType)
        {
            var clip = _soundsConfig[soundType];
            _audioSource.PlayOneShot(clip);
        }
    }
}