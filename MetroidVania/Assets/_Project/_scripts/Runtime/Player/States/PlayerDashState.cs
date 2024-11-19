using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerDashState : BaseState
    {
        private MonoBehaviour _mono;

        private PlayerDashConfig config;
        private Vector2 _movement;

        bool isDashing;
        
        public PlayerDashState(MonoBehaviour mono, PlayerDashConfig config, PlayerController controller, PlayerControls controls, Rigidbody rigidbody, Transform transform)
        {
            _mono = mono;
            this.config = config;
            config.SetController(controller);
            config.SetControls(controls);
            config.SetRigidbody(rigidbody);
            config.SetTransform(transform);
        }

        public override void DoChecks()
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState()
        {
            //config.GetControls.Player.Move.performed += OnMove;
            //config.GetControls.Player.Move.canceled += OnMove;

            isDashing = true;
            _mono.StartCoroutine(Dash());
        }

        public override void ExitState()
        {
            //config.GetControls.Player.Move.performed -= OnMove;
            //config.GetControls.Player.Move.canceled -= OnMove;
        }

        public override void FixedUpdateState()
        {
            
        }

        public override void UpdateState()
        {
            if(!isDashing) { config.GetController.SwitchState(config.GetController.movementState); }
        }

        //private void OnMove(InputAction.CallbackContext context)
        //{
        //    _movement = context.ReadValue<Vector2>();
        //}

        IEnumerator Dash()
        {
            Debug.Log(_movement);
            config.GetRigidbody.linearVelocity = config.GetRigidbody.linearVelocity * config.GetDashStrength;
            yield return new WaitForSeconds(config.GetDashLength);
            isDashing = false;
        }
    }
}
