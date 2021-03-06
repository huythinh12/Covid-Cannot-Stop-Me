using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform camera;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float speed;
        //pending
        // [SerializeField] private float jumpForece = 2.5f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float turnSmoothVelocity;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private GameObject dirtyParticleFoot;

        public static bool onGrounded;
        public static bool isAiming;
        public static bool hasKeyJump;
        public static bool isRunning;
        public static bool isMove;
        public GameObject followTarget;
        public Quaternion nextRotation;
        
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
            rbPlayer = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            CheckGround();
            Movement();
        }

        private void CheckGround()
        {
            RaycastHit hitinfo;
            onGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out hitinfo, groundCheckRadius);
        }

        private void Movement()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;
            //rotate when aim 
            if (isAiming)
            {
                float xAxisMouse = Input.GetAxisRaw("Mouse X"); // tr??? v??? gi?? tr???[-1..1]
                float yAxisMouse = Input.GetAxisRaw("Mouse Y") * -1f;

                //xoay followTarget(v??? tr?? object ??? c??? ) theo tr???c y th???ng ?????ng -> nh??n sang tr??i/ph???i
                followTarget.transform.rotation *= Quaternion.AngleAxis(xAxisMouse * rotationPower, Vector3.up);
                //xoay followTarget(v??? tr?? object ??? c??? ) theo tr???c x n???m ngang -> nh??n xu???ng/l??n
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
                //ki???m tra tr??nh b??? l???i n???u ko s??? d???ng aim th?? ch??? n??y s??? ko c???n ph???i ch???y v?? 2 tr?????ng h???p n??y l?? kh??c nhau 1 c??i d??ng cho transform rotate 1 cai aim d??ng cho followtarget 
                if (!isAiming)
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 playerMovementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.position += playerMovementDirection * speed * Time.deltaTime;

                //check to running
                if (Input.GetKey(KeyCode.LeftShift) && onGrounded)
                {
                    isRunning = true;
                    isMove = false;
                    dirtyParticleFoot.SetActive(true);
                    //pending
                    // if (Input.GetKeyDown(KeyCode.Space) && onGrounded) // check when running to jump 
                    // {
                    //     hasKeyJump = true;
                    //     rbPlayer.AddForce(Vector3.up * jumpForece, ForceMode.Impulse);
                    // }
                    float speedUp = speed * 2;
                    transform.position += playerMovementDirection * speedUp * Time.deltaTime;
                }
                else
                {
                    isMove = true;
                    isRunning = false;
                    dirtyParticleFoot.SetActive(false);
                }
            }
            else if (!onGrounded || direction.magnitude < 0.1f)
            {
                dirtyParticleFoot.SetActive(false);
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

    //C??ch c?? 
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