using UnityEngine;
using UnityEngine.InputSystem;

namespace Vania.Runtime.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 _movement;
        private Rigidbody _rb;

        [Header("Snap to ground")]
        [SerializeField] private float groundDist;
        [SerializeField] private LayerMask groundLayer;

        [Header("Movement")]
        [SerializeField] private float movementSpeed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            SnapToGround();
        }

        private void SnapToGround()
        {
            RaycastHit hit;
            Vector3 pos = transform.position;
            pos.y += 1;
            if (Physics.Raycast(pos, -transform.up, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.collider != null)
                {
                    Vector3 movePos = transform.position;
                    movePos.y = hit.point.y + groundDist;
                    transform.position = movePos;
                }
            }
        }

        private void FixedUpdate()
        {
            Vector3 moveDir = new Vector3(_movement.x, 0, _movement.y);
            _rb.linearVelocity = moveDir * movementSpeed;
        }

        private void OnMove(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
    }
}