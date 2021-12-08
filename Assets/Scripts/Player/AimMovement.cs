using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMovement : MonoBehaviour
{
    public GameObject followTarget;
    public Quaternion nextRotation;
    public Vector3 nextPosition;
    public float speed = 1f;

    [SerializeField] private Transform camera;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    private float rotationPower = 3f;
    private float rotationSmoothToLerp = 0.5f;
    private Vector3 angles;


    // Update is called once per frame
    void Update()
    {
        float xAxisMouse = Input.GetAxisRaw("Mouse X"); // trả về giá trị[-1..1]
        float yAxisMouse = Input.GetAxisRaw("Mouse Y");

        //xoay followTarget(vị trí object ở cổ ) theo trục y thẳng đứng -> nhìn sang trái/phải
        followTarget.transform.rotation *= Quaternion.AngleAxis(xAxisMouse * rotationPower, Vector3.up);

        //xoay followTarget(vị trí object ở cổ ) theo trục x nằm ngang -> nhìn xuống/lên
        followTarget.transform.rotation *= Quaternion.AngleAxis(yAxisMouse * rotationPower, Vector3.right);

        ClampUpDownRotation();

        nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation,
            Time.deltaTime * rotationSmoothToLerp);

        float xAxisKey = Input.GetAxisRaw("Horizontal"); // trả về giá trị[-1..1]
        float yAxisKey = Input.GetAxisRaw("Vertical"); // không bấm sẽ là 0
        
        //get nextPosition to move 
         float moveSpeed = speed / 100f;
        
         // Vector3 position = (transform.forward * yAxisKey * moveSpeed) + (transform.right * xAxisKey * moveSpeed);
         // nextPosition = transform.position + position;
       //  print(transform.position + position +" foward transform");
         
         transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
         followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        // transform.position = nextPosition;

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