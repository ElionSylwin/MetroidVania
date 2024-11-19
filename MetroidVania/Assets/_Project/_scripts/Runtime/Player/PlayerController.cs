using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Interfaces;
using Vania.Runtime.Core;
using System.Collections.Generic;

namespace Vania.Runtime.Player
{
    public class PlayerController : BaseStateMachine
    {
        [SerializeField] private BaseState _currentState;

        #region States
        public BaseState movementState;
        public BaseState jumpState;
        public BaseState dashState;
        #endregion

        [Header("Configs")]
        [SerializeField] private PlayerMovementConfig _movementConfig;
        [SerializeField] private PlayerJumpConfig _jumpConfig;
        [SerializeField] private PlayerDashConfig _dashConfig;

        [Header("Data")]
        [SerializeField] private PlayerControls _controls;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _transform;

        [SerializeField] private InputActionReference _Interact;

        [SerializeField] private bool canInteract = false;
        [SerializeField] private IInteractable interactableInstance;

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void Start()
        {
            movementState = new PlayerMovementState(_movementConfig, this, _controls, _rigidbody, _transform);
            jumpState = new PlayerJumpState(this, _jumpConfig, this, _controls, _rigidbody, _transform);
            dashState = new PlayerDashState(this, _dashConfig, this, _controls, _rigidbody,_transform);

            SwitchState(movementState);
        }

        private void OnEnable()
        {
            _controls.Player.Enable();

            _Interact.action.performed += TryToInteract;
            _Interact.action.Enable();
        }

        private void OnDisable()
        {
            _Interact.action.performed -= TryToInteract;
            _Interact.action.Disable();
        }

        private void TryToInteract(InputAction.CallbackContext context)
        {
            Debug.Log("interact");
            if(interactableInstance != null && canInteract)
            {
                interactableInstance.Interact();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Interactable"))
            {
                Debug.Log("Got tag");
                canInteract = true;
                interactableInstance = other.GetComponent<IInteractable>();
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            canInteract = false;
            interactableInstance = null;
        }

        public override void Update()
        {
            _currentState.UpdateState();
        }

        public override void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }

        public void SwitchState(BaseState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

    }
}