using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int maxDef;
    public static int currentHealth;
    public static int currentDef;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Virus"))
        {
           
        }
    }

    private void OnDisable()
    {
        currentHealth = maxHealth;
        currentDef = maxDef;
    }
}
