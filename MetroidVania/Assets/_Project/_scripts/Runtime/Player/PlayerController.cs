using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Interfaces;
using Vania.Runtime.Core;

namespace Vania.Runtime.Player
{
    public class PlayerController : BaseStateMachine
    {
        [SerializeField] private PlayerState _currentState;

        //States
        private PlayerState movementState;
        
        [SerializeField] private Rigidbody _rb;
        


        [SerializeField] private InputActionReference _Interact;

        [SerializeField] private bool canInteract = false;
        [SerializeField] private IInteractable interactableInstance;

        [SerializeField] private PlayerControls _controls;
        private Vector2 _movement;

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void Start()
        {
            movementState = new PlayerMovementState(this,_rb,transform);

            SwitchState(movementState);
        }

        private void OnEnable()
        {
            _controls.Player.Enable();

            _controls.Player.Move.performed += OnMove;
            _controls.Player.Move.canceled += OnMove;

            _Interact.action.performed += TryToInteract;
            _Interact.action.Enable();
        }

        private void OnDisable()
        {
            _Interact.action.performed -= TryToInteract;
            _Interact.action.Disable();

            _controls.Player.Move.performed -= OnMove;
            _controls.Player.Move.canceled -= OnMove;
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
            Debug.LogWarning(other.gameObject.name);
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

        public void SwitchState(PlayerState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState(this);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
            _currentState.OnMoveInput(_movement);
        }
    }
}