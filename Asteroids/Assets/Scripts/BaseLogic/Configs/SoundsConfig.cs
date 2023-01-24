using System.Collections.Generic;
using Asteroids.Enums;
using UnityEngine;

namespace Asteroids.Configs
{
    [CreateAssetMenu(fileName = nameof(SoundsConfig), menuName = "Configs/" +nameof(SoundsConfig))]
    public class SoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioClip blasterSound;
        [SerializeField] private AudioClip bulletSound;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private float volume;
        [SerializeField] private bool initialVolumeState;
        private Dictionary<SoundType, AudioClip> _clipsDictionary;
            
        public AudioClip this[SoundType soundType] => _clipsDictionary[soundType];
        public float Volume => volume;
        public bool InitialVolumeState => initialVolumeState;
        public void OnCreat()
        {
            _clipsDictionary = new Dictionary<SoundType, AudioClip>()
            {
                { SoundType.RaySound, blasterSound},
                { SoundType.BulletSound, bulletSound },
                { SoundType.ExplosionSound, explosionSound}
            };
        }
        
    }
}