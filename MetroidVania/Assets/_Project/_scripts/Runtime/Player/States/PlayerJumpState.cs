using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerJumpState : BaseState
    {
        private MonoBehaviour _mono;
        private PlayerJumpConfig config;
        private bool isGrounded;
        private bool canDoubleJump = false;

        public PlayerJumpState(MonoBehaviour mono,PlayerJumpConfig config, PlayerController controller, PlayerControls controls, Rigidbody rigidbody, Transform transform)
        {
            _mono = mono;
            this.config = config;
            config.SetController(controller);
            config.SetControls(controls);
            config.SetRigidbody(rigidbody);
            config.SetTransform(transform);
        }

        public override void EnterState()
        {
            Jump();
            _mono.StartCoroutine(DoubleJumpWindow());
        }

        public override void FixedUpdateState()
        {
        }

        public override void UpdateState()
        {
            if (config.GetRigidbody.linearVelocity.y == 0)
            {
                DoChecks();
                if(isGrounded) { config.GetController.SwitchState(config.GetController.movementState); }
            }
        }

        public override void DoChecks()
        {
            RaycastHit hit;
            if (Physics.Raycast(config.GetTransform.position, -config.GetTransform.up, out hit, config.GetGroundRayDistance, config.GetLayerMask))
            {
                if (hit.collider != null)
                {
                    isGrounded = true;
                    canDoubleJump = false;
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        public override void ExitState()
        {
            _mono.StopAllCoroutines();
            config.GetControls.Player.Jump.performed -= DoubleJump;
        }

        private IEnumerator DoubleJumpWindow()
        {
            canDoubleJump = true;

            config.GetControls.Player.Jump.performed += DoubleJump;
            yield return new WaitForSeconds(config.DoubleJumpWindow);
            canDoubleJump = false;
            config.GetControls.Player.Jump.performed -= DoubleJump;
        }

        private void DoubleJump(InputAction.CallbackContext context)
        {
            if(canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }    
        }

        private void Jump()
        {
            config.GetRigidbody.linearVelocity = new Vector2(config.GetRigidbody.linearVelocity.x, config.JumpStrength);
        }
    }
}
