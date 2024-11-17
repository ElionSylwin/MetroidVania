using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerMovementState : PlayerState
    {
        private PlayerController controller;

        //TEMP until config
        private Vector2 _movement;
        private Rigidbody _rb;
        private Transform _transform;


        [Header("Snap to ground")]
        [SerializeField] private float groundDist;
        [SerializeField] private LayerMask groundLayer;

        [Header("Movement")]
        [SerializeField] private float movementSpeed;

        public PlayerMovementState(PlayerController controller, Rigidbody rb, Transform transform)
        {
            this.controller = controller;
            _rb = rb;
            _transform = transform;
        }

        public override void EnterState(BaseStateMachine stateMachine)
        {
            movementSpeed = 2f;
        }

        public override void UpdateState()
        {
            SnapToGround();
        }

        public override void FixedUpdateState()
        {
            Vector3 moveDir = new Vector3(_movement.x, 0, _movement.y);
            _rb.linearVelocity = moveDir * movementSpeed;
        }

        public override void DoChecks()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void OnMoveInput(Vector2 input)
        {
            _movement = input;
        }

        private void SnapToGround()
        {
            RaycastHit hit;
            Vector3 pos = _transform.position;
            pos.y += 1;
            if (Physics.Raycast(pos, -_transform.up, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.collider != null)
                {
                    Vector3 movePos = _transform.position;
                    movePos.y = hit.point.y + groundDist;
                    _transform.position = movePos;
                }
            }
        }

        private void OnMove(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
    }
}
