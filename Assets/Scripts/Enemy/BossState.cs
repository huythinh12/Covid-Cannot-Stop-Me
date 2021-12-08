using UnityEngine;
public class BossState : MonoBehaviour
{
    public static bool isGetHit;
    public static int health = 3;


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            Destroy(other.collider.gameObject);
            health--;
            isGetHit = true;
        }
    }

  
    
  
}