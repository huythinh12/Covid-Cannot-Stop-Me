using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform camera;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForece = 2.5f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float turnSmoothVelocity;
        [SerializeField] private float groundDistance;

        public static bool onGrounded;
        public static bool isAiming;
        public static bool hasKeyJump;
        public GameObject characterAnimtion;
        public GameObject followTarget;
        public Quaternion nextRotation;

        private AnimationsEvent animationEvent;
        private Rigidbody rbPlayer;
        private float rotationPower = 3f;
        private float rotationSmoothToLerp = 0.2f;
        private Vector3 angles;


        #region CharacterController-OldCase

        // [SerializeField] private CharacterController playerCharacterController;
        // [SerializeField] Vector3 velocity;
        // [SerializeField] private float gravity = -9.81f;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            animationEvent = characterAnimtion.GetComponent<AnimationsEvent>();
            rbPlayer = GetComponent<Rigidbody>();
            groundMask = LayerMask.NameToLayer("Ground");
        }

        private void FixedUpdate()
        {
            CheckGround();
            Movement();
        }

        private void CheckGround()
        {
            onGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~groundMask);
        }

        private void Movement()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;

            //rotate when aim 
            if (isAiming)
            {
                float xAxisMouse = Input.GetAxisRaw("Mouse X"); // trả về giá trị[-1..1]
                float yAxisMouse = Input.GetAxisRaw("Mouse Y") * -1f;

                //xoay followTarget(vị trí object ở cổ ) theo trục y thẳng đứng -> nhìn sang trái/phải
                followTarget.transform.rotation *= Quaternion.AngleAxis(xAxisMouse * rotationPower, Vector3.up);
                //xoay followTarget(vị trí object ở cổ ) theo trục x nằm ngang -> nhìn xuống/lên
                followTarget.transform.rotation *= Quaternion.AngleAxis(yAxisMouse * rotationPower, Vector3.right);

                ClampUpDownRotation();

                //rotate theo aim 
                nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation,
                    Time.deltaTime * rotationSmoothToLerp);
                transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
                followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            // kiem tra neu co di chuyen             
            if (direction.magnitude >= 0.1f)
            {
                // Charater Direction same as Camera Forward
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                //kiểm tra tránh bị lỗi nếu ko sử dụng aim thì chỗ này sẽ ko cần phải chạy vì 2 trường hợp này là khác nhau 1 cái dùng cho transform rotate 1 cai aim dùng cho followtarget 
                if (!isAiming)
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 playerMovementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.position += playerMovementDirection * speed * Time.deltaTime;
                
                //check to running
                if (Input.GetKey(KeyCode.LeftShift) && !isAiming)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && onGrounded) // check when running to jump 
                    {
                        hasKeyJump = true;
                        rbPlayer.AddForce(Vector3.up * jumpForece, ForceMode.Impulse);
                    }
                    float speedUp = speed * 2;
                    transform.position += playerMovementDirection * speedUp * Time.deltaTime;
                }
            }
        }
        
        
        private void ClampUpDownRotation()
        {
            //get all angel
            angles = followTarget.transform.localEulerAngles;
            angles.z = 0;
            //get angle x 
            var angle = followTarget.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

            followTarget.transform.localEulerAngles = angles;
        }
    }
    
    //Cách cũ 
    //Jump follow by velocity
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

}