using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerMoveHandler : IFixedTickable
    {
        readonly LevelBoundary _levelBoundary;
        readonly Settings _settings;
        readonly InjectedPlayer _injectedPlayer;
        readonly PlayerInputState _inputState;

        public PlayerMoveHandler(
            PlayerInputState inputState,
            InjectedPlayer injectedPlayer,
            Settings settings,
            LevelBoundary levelBoundary)
        {
            _levelBoundary = levelBoundary;
            _settings = settings;
            _injectedPlayer = injectedPlayer;
            _inputState = inputState;
        }

        public void FixedTick()
        {
            if (_injectedPlayer.IsDead)
            {
                return;
            }

            if (_inputState.IsMovingLeft)
            {
                _injectedPlayer.AddForce(
                    Vector3.left * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingRight)
            {
                _injectedPlayer.AddForce(
                    Vector3.right * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingUp)
            {
                _injectedPlayer.AddForce(
                    Vector3.up * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingDown)
            {
                _injectedPlayer.AddForce(
                    Vector3.down * _settings.MoveSpeed);
            }

            // Always ensure we are on the main plane
            _injectedPlayer.Position = new Vector3(_injectedPlayer.Position.x, _injectedPlayer.Position.y, 0);

            KeepPlayerOnScreen();
        }

        void KeepPlayerOnScreen()
        {
            var extentLeft = (_levelBoundary.Left + _settings.BoundaryBuffer) - _injectedPlayer.Position.x;
            var extentRight = _injectedPlayer.Position.x - (_levelBoundary.Right - _settings.BoundaryBuffer);

            if (extentLeft > 0)
            {
                _injectedPlayer.AddForce(
                    Vector3.right * _settings.BoundaryAdjustForce * extentLeft);
            }
            else if (extentRight > 0)
            {
                _injectedPlayer.AddForce(
                    Vector3.left * _settings.BoundaryAdjustForce * extentRight);
            }

            var extentTop = _injectedPlayer.Position.y - (_levelBoundary.Top - _settings.BoundaryBuffer);
            var extentBottom = (_levelBoundary.Bottom + _settings.BoundaryBuffer) - _injectedPlayer.Position.y;

            if (extentTop > 0)
            {
                _injectedPlayer.AddForce(
                    Vector3.down * _settings.BoundaryAdjustForce * extentTop);
            }
            else if (extentBottom > 0)
            {
                _injectedPlayer.AddForce(
                    Vector3.up * _settings.BoundaryAdjustForce * extentBottom);
            }
        }

        [Serializable]
        public class Settings
        {
            public float BoundaryBuffer;
            public float BoundaryAdjustForce;
            public float MoveSpeed;
            public float SlowDownSpeed;
        }
    }
}
