using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerDamageHandler
    {
        readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly InjectedPlayer _injectedPlayer;

        public PlayerDamageHandler(
            InjectedPlayer injectedPlayer,
            Settings settings,
            AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _settings = settings;
            _injectedPlayer = injectedPlayer;
        }

        public void TakeDamage(Vector3 moveDirection)
        {
            _audioPlayer.Play(_settings.HitSound, _settings.HitSoundVolume);

            _injectedPlayer.AddForce(-moveDirection * _settings.HitForce);

            _injectedPlayer.TakeDamage(_settings.HealthLoss);
        }

        [Serializable]
        public class Settings
        {
            public float HealthLoss;
            public float HitForce;

            public AudioClip HitSound;
            public float HitSoundVolume = 1.0f;
        }
    }
}
