using UnityEngine;
using UnityEngine.UIElements;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerJumpState : BaseState
    {
        private PlayerJumpConfig config;
        private bool isGrounded;

        public PlayerJumpState(PlayerJumpConfig config, PlayerController controller, PlayerControls controls, Rigidbody rigidbody, Transform transform)
        {
            this.config = config;
            config.SetController(controller);
            config.SetControls(controls);
            config.SetRigidbody(rigidbody);
            config.SetTransform(transform);
        }

        public override void EnterState()
        {
            config.GetRigidbody.linearVelocity = new Vector3(config.GetRigidbody.linearVelocity.x, config.JumpStrength);
        }

        public override void FixedUpdateState()
        {
            Debug.Log("Jump state");
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
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        public override void ExitState()
        {
            
        }
    }
}
