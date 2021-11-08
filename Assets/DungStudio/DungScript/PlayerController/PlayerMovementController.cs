using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        
        [SerializeField] private Transform camera;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForece = 3f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float turnSmoothVelocity;
        [SerializeField] private float groundDistance = 0.01f;
        [SerializeField] private bool IsGrounded;

        private Rigidbody rbPlayer;
        
        #region CharacterController-OldCase
        // [SerializeField] private CharacterController playerCharacterController;
        // [SerializeField] Vector3 velocity;
        // [SerializeField] private float gravity = -9.81f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            DectectCollisions();
            rbPlayer = GetComponent<Rigidbody>();
            groundMask = LayerMask.NameToLayer("Ground");
        }

        private void FixedUpdate()
        {
            PlayerJump();
            PlayerMovement();
        }

        private void DectectCollisions()
        {
            // playerCharacterController.detectCollisions = false;
        }

        private void PlayerMovement()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Charater Direction same as Camera Forward
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 playerMovementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                
                transform.position += playerMovementDirection * speed * Time.deltaTime;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    float speedUp = speed * 2;
                    transform.position += playerMovementDirection * speedUp * Time.deltaTime;
                }

                #region CharacterController-OldCase
                //Cách cũ sử dụng Character Controller
                // PlayerMovement on condition input
                // if (Input.GetKey(KeyCode.LeftShift))
                // {
                //     speed = 4;
                //     
                //     playerCharacterController.Move(playerMovementDirection.normalized * speed * Time.deltaTime);
                // }
                // else
                // {
                //     speed = 1.5f;
                //     playerCharacterController.Move(playerMovementDirection.normalized * speed * Time.deltaTime);
                // }

                #endregion    
            }
        }

        private void PlayerJump()
        {
            IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~groundMask);
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                rbPlayer.AddForce(Vector3.up * jumpForece ,ForceMode.Impulse);
            }

            #region CharacterController-OldCase
            // Cách cũ sử dụng Character Controller
            // if (IsGrounded && velocity.y < 0)
            // {
            //   velocity.y = -2f;
            // }
            // if (Input.GetKey(KeyCode.Space) && IsGrounded)
            // {
            //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // }
            // velocity.y += gravity * Time.deltaTime;
            //
            // playerCharacterController.Move(velocity.normalized  * Time.deltaTime);
            #endregion
        }
        
    }
}