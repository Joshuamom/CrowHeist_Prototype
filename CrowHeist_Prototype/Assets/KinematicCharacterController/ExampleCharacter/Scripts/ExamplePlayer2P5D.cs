using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer2P5D : MonoBehaviour
    {
        // Public fields to adjust in the Unity Inspector
        public float moveSpeed = 5f;
        public float jumpForce = 8f;
        public Transform groundCheck;
        public LayerMask groundLayer;

        // Private fields
        private Rigidbody rb;
        private bool isGrounded;
        private float groundCheckRadius = 0.2f;

        void Start()
        {
            // Get the Rigidbody component
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            // Handle movement
            Move();

            // Check if the player is grounded before jumping
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius);
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }

        private void Move()
        {
            // Get horizontal input (e.g., A/D keys or arrow keys)

            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

            // Apply movement
            rb.velocity = moveInput * moveSpeed;

            // Rotate the character to face the movement direction
            if (moveInput != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveInput);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }

        private void Jump()
        {
            // Apply an upward force to the Rigidbody for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        void OnDrawGizmosSelected()
        {
            // Visualize the ground check in the Scene view
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}