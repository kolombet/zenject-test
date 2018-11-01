using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class InjectedPlayer
    {
        readonly Rigidbody _rigidBody;
        readonly MeshRenderer _renderer;
        readonly SignalBus _signalBus;

        float _health = 100.0f;
        float _exp = 100;

        public InjectedPlayer(
            Rigidbody rigidBody,
            MeshRenderer renderer,
            SignalBus signalBus)
        {
            _rigidBody = rigidBody;
            _renderer = renderer;
            _signalBus = signalBus;
            _signalBus.Subscribe<EnemyKilledSignal>(OnEnemyKilled);
            Debug.Log("isinj");
        }
        
        void OnEnemyKilled()
        {
            _exp += 10;
        }

        public MeshRenderer Renderer
        {
            get { return _renderer; }
        }

        public bool IsDead
        {
            get; set;
        }

        public float Health
        {
            get { return _health; }
        }

        public float Exp
        {
            get { return _exp; }
        }

        public Vector3 LookDir
        {
            get { return -_rigidBody.transform.right; }
        }

        public Quaternion Rotation
        {
            get { return _rigidBody.rotation; }
            set { _rigidBody.rotation = value; }
        }

        public Vector3 Position
        {
            get { return _rigidBody.position; }
            set { _rigidBody.position = value; }
        }

        public Vector3 Velocity
        {
            get { return _rigidBody.velocity; }
        }

        public void TakeDamage(float healthLoss)
        {
            _health = Mathf.Max(0.0f, _health - healthLoss);
        }

        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }
    }
}
