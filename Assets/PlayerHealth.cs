using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider health, def;
    //show to edit 
    public int maxHealth;
    public int maxDef;
    
    public static bool isEmptyHP;
    public static int currentHealth;
    public static int currentDef;

    private void OnEnable()
    {
        //UI
        health.maxValue = maxHealth;
        health.value = maxHealth;
        def.value = maxDef;
        def.maxValue = maxDef;

        currentHealth = maxHealth;
        currentDef = maxDef;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Virus"))
        {
            if (currentDef > 0)
            {
                currentDef--;
                def.value = currentDef;
            }
            else
            {
                if (currentHealth > 0)
                {
                    currentHealth--;
                    health.value = currentHealth;
                }
                else
                {
                    isEmptyHP = true;
                }
            }
        }
    }

    private void OnDisable()
    {
        currentHealth = maxHealth;
        currentDef = maxDef;
        isEmptyHP = false;
    }
}