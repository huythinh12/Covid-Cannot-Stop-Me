using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController playerCharacterController;
        [SerializeField]
        private Transform camera;
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private LayerMask groundMask;
        [SerializeField]
        private float speed = 4f;
        [SerializeField]
        private float jumpHeight = 3f;
        [SerializeField]
        private float turnSmoothTime = 0.1f;
        [SerializeField]
        private float turnSmoothVelocity;
        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private float gravity = -9.81f;
        [SerializeField]
        Vector3 velocity;
        [SerializeField]
        bool IsGrounded;

        // Start is called before the first frame update
        void Start()
        {
            DectectCollisions();
        }

        private void FixedUpdate()
        {
            PlayerMovement();
            PlayerJump();
        }

        private void DectectCollisions()
        {
            playerCharacterController.detectCollisions = false;
        }
        private void PlayerMovement()
        {
           
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;

            if(direction.magnitude >= 0.1f)
            {
                // Charater Direction same as Camera Forward
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothVelocity);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 playerMovementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // PlayerMovement on condition input
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 4;
                    playerCharacterController.Move(playerMovementDirection.normalized * speed * Time.deltaTime);
                }
                else
                {
                    speed = 1.5f;
                    playerCharacterController.Move(playerMovementDirection.normalized * speed * Time.deltaTime);
                }
            }
        }

        private void PlayerJump()
        {
            IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (IsGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetKey(KeyCode.Space) && IsGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity)* Time.deltaTime;
            }
            velocity.y += gravity * Time.deltaTime;
            playerCharacterController.Move(velocity.normalized * Time.deltaTime);
        }    
    }
}