using UnityEngine;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class BasePlayerConfig : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] protected PlayerController _controller;
        [SerializeField] protected PlayerControls _controls;
        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected Transform _transform;

        [Header("Ground Check")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundRayDistance;


        public PlayerController GetController { get { return _controller; } }
        public Rigidbody GetRigidbody { get {  return _rb; } }
        public PlayerControls GetControls { get { return _controls; } }
        public Transform GetTransform { get { return _transform;} }
        public LayerMask GetLayerMask { get { return groundLayer; } }
        public float GetGroundRayDistance {  get { return groundRayDistance; } }

        public void SetController(PlayerController controller) { _controller = controller; }
        public void SetControls(PlayerControls controls) {  _controls = controls; }
        public void SetRigidbody(Rigidbody rb) { _rb = rb;}
        public void SetTransform(Transform transform) { _transform = transform; }
    }
}
