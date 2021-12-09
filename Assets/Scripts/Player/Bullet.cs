using Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int throwForce;
    
    // [SerializeField] private GameObject throwPoint;
    [SerializeField] private GameObject explosionFX;
    private Vector3 aimDir;
    private Rigidbody rbBall;
    private GameObject throwPoint;

    void Start()
    {
        throwPoint = GameObject.FindGameObjectWithTag("ThrowPoint");
        rbBall = GetComponent<Rigidbody>();
        transform.parent = throwPoint.transform;
        transform.position = throwPoint.transform.position;
        
    }

    public void ReleaseMe()
    {
        transform.parent = null;
        rbBall.useGravity = true;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        transform.rotation = throwPoint.transform.rotation;
        rbBall.AddForce(AimController.aimDir * throwForce, ForceMode.Impulse);
        Invoke("RemoveDelay",3);
    }

    private void RemoveDelay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}