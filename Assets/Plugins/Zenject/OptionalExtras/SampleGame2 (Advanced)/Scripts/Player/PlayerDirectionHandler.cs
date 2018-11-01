using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerDirectionHandler : ITickable
    {
        readonly InjectedPlayer _injectedPlayer;
        readonly Camera _mainCamera;

        public PlayerDirectionHandler(
            Camera mainCamera,
            InjectedPlayer injectedPlayer)
        {
            _injectedPlayer = injectedPlayer;
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

            var mousePos = mouseRay.origin;
            mousePos.z = 0;

            var goalDir = mousePos - _injectedPlayer.Position;
            goalDir.z = 0;
            goalDir.Normalize();

            _injectedPlayer.Rotation = Quaternion.LookRotation(goalDir) * Quaternion.AngleAxis(90, Vector3.up);
        }
    }
}
