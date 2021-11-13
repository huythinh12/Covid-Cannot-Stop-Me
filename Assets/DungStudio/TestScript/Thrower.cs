using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private GameObject theBall;
    [SerializeField] private BallScript ballScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ThrowBall()
    {
        Debug.Log("Throwing!!!");
        ballScript.ReleaseMe();
           
    }
}
