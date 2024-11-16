using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vania.Runtime.Interfaces;

namespace Vania.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _Interact;

        [SerializeField] private bool canInteract = false;
        [SerializeField] private IInteractable interactableInstance;


        private void OnEnable()
        {
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
    }
}