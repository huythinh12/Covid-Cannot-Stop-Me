using Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int throwForce;
    [SerializeField] private GameObject throwPoint;
    private Vector3 aimDir;
    private Collider collider;
    private Rigidbody rbBall;
    
    void Start()
    {
        collider = GetComponent<Collider>();
        rbBall = GetComponent<Rigidbody>();
        transform.parent = throwPoint.transform;
    }

    public void ReleaseMe()
    {
        transform.parent = null;
        rbBall.useGravity = true;
        transform.rotation = throwPoint.transform.rotation;
        rbBall.AddForce(AimController.aimDir * throwForce, ForceMode.Impulse);
    }
}