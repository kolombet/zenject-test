using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerHealthWatcher : ITickable
    {
        readonly SignalBus _signalBus;
        readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly Explosion.Factory _explosionFactory;
        readonly InjectedPlayer _injectedPlayer;

        public PlayerHealthWatcher(
            InjectedPlayer injectedPlayer,
            Explosion.Factory explosionFactory,
            Settings settings,
            AudioPlayer audioPlayer,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
            _settings = settings;
            _explosionFactory = explosionFactory;
            _injectedPlayer = injectedPlayer;
        }

        public void Tick()
        {
            if (_injectedPlayer.Health <= 0 && !_injectedPlayer.IsDead)
            {
                Die();
            }
        }

        void Die()
        {
            _injectedPlayer.IsDead = true;

            var explosion = _explosionFactory.Create();
            explosion.transform.position = _injectedPlayer.Position;

            _injectedPlayer.Renderer.enabled = false;

            _signalBus.Fire<PlayerDiedSignal>();

            _audioPlayer.Play(_settings.DeathSound, _settings.DeathSoundVolume);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip DeathSound;
            public float DeathSoundVolume = 1.0f;
        }
    }
}
