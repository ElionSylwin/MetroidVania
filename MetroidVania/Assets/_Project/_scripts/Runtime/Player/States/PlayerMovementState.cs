using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerMovementState : BaseState
    {

        private PlayerMovementConfig config;
        private bool isGrounded;
        private Vector2 _movement;

        public PlayerMovementState(PlayerMovementConfig config,PlayerController controller, PlayerControls controls, Rigidbody rigidbody, Transform transform)
        {
            this.config = config;
            config.SetController(controller);
            config.SetControls(controls);
            config.SetRigidbody(rigidbody);
            config.SetTransform(transform);
        }

        public override void EnterState()
        {

            config.GetControls.Player.Move.performed += OnMove;
            config.GetControls.Player.Move.canceled += OnMove;

            config.GetControls.Player.Jump.performed += Jump;
            if (!config.GetControls.Player.Move.inProgress) { _movement = new Vector2(0, 0); }
        }

        public override void UpdateState()
        {
            //SnapToGround();
            Debug.LogWarning(isGrounded);
            Debug.Log("Movement State");
            DoChecks();
        }

        public override void FixedUpdateState()
        {
            Vector3 moveDir = new Vector3(_movement.x, 0, _movement.y);
            config.GetRigidbody.linearVelocity = moveDir * config.MovementSpeed;
        }

        public override void DoChecks()
        {
            RaycastHit hit;
            if (Physics.Raycast(config.GetTransform.position, -config.GetTransform.up, out hit, config.GetGroundRayDistance, config.GetLayerMask))
            {
                if (hit.collider != null)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        public override void ExitState()
        {
            config.GetControls.Player.Move.performed -= OnMove;
            config.GetControls.Player.Move.canceled -= OnMove;

            config.GetControls.Player.Jump.performed -= Jump;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        //private void SnapToGround()
        //{
        //    RaycastHit hit;
        //    Vector3 pos = config.GetTransform.position;
        //    pos.y += 1;
        //    if (Physics.Raycast(pos, -config.GetTransform.up, out hit, Mathf.Infinity, config.GroundLayer))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Vector3 movePos = config.GetTransform.position;
        //            movePos.y = hit.point.y + config.GroundDist;
        //            config.GetTransform.position = movePos;
        //        }
        //    }
        //}

        private void Jump(InputAction.CallbackContext context)
        {
            if (isGrounded) {config.GetController.SwitchState(config.GetController.jumpState); }
        }
    }
}
