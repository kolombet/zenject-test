using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerShootHandler : ITickable
    {
        readonly AudioPlayer _audioPlayer;
        readonly InjectedPlayer _injectedPlayer;
        readonly Settings _settings;
        readonly Bullet.Factory _bulletFactory;
        readonly PlayerInputState _inputState;

        float _lastFireTime;

        public PlayerShootHandler(
            PlayerInputState inputState,
            Bullet.Factory bulletFactory,
            Settings settings,
            InjectedPlayer injectedPlayer,
            AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _injectedPlayer = injectedPlayer;
            _settings = settings;
            _bulletFactory = bulletFactory;
            _inputState = inputState;
        }

        public void Tick()
        {
            if (_injectedPlayer.IsDead)
            {
                return;
            }

            if (_inputState.IsFiring && Time.realtimeSinceStartup - _lastFireTime > _settings.MaxShootInterval)
            {
                _lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        void Fire()
        {
            _audioPlayer.Play(_settings.Laser, _settings.LaserVolume);

            var bullet = _bulletFactory.Create(
                _settings.BulletSpeed, _settings.BulletLifetime, BulletTypes.FromPlayer);

            bullet.transform.position = _injectedPlayer.Position + _injectedPlayer.LookDir * _settings.BulletOffsetDistance;
            bullet.transform.rotation = _injectedPlayer.Rotation;
        }

        [Serializable]
        public class Settings
        {
            public AudioClip Laser;
            public float LaserVolume = 1.0f;

            public float BulletLifetime;
            public float BulletSpeed;
            public float MaxShootInterval;
            public float BulletOffsetDistance;
        }
    }
}
