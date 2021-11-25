using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Slider health, def;

    //show to edit 
    public int maxHealth;
    public int maxDef;
    public static UnityEvent<int, int> OnGetHit = new UnityEvent<int, int>();
    public static bool isEmptyHP;
    public static int currentHealth;
    public static int currentDef;

    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
        def = GameObject.FindGameObjectWithTag("Def").GetComponent<Slider>();
    }

    private void Start()
    {
        //UI
        if (def != null && health != null)
        {
            health.maxValue = maxHealth;
            health.value = maxHealth;
            def.value = maxDef;
            def.maxValue = maxDef;
        }
        currentHealth = maxHealth;
        currentDef = maxDef;
    }

    private void OnEnable()
    {
        SupportTarget.OnHealing?.AddListener(GetHealing);
        SupportTargetDef.OnBuffDef?.AddListener(GetDef);
    }

    private void Update()
    {
        if (currentHealth <=0)
        {
            SupportTarget.OnHealing?.RemoveListener(GetHealing);
            SupportTargetDef.OnBuffDef?.RemoveListener(GetDef);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth--;
            health.value = currentHealth;
            OnGetHit?.Invoke(currentDef, currentHealth);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Virus"))
        {
            if (currentDef > 0)
            {
                currentDef--;
                OnGetHit?.Invoke(currentDef, currentHealth);
                if (def != null)
                    def.value = currentDef;
            }
            else
            {
                if (currentHealth > 0)
                {
                    currentHealth--;
                    OnGetHit?.Invoke(currentDef, currentHealth);
                    if (health != null)
                        health.value = currentHealth;
                }
                else
                {
                    isEmptyHP = true;
                }
            }
        }
    }
    private void GetHealing()
    {
        currentHealth ++;
        this.health.value = currentHealth;
    }

    private void GetDef()
    {
        currentDef++;
        this.def.value = currentDef;
    }

    private void OnDisable()
    {
        SupportTarget.OnHealing?.RemoveListener(GetHealing);
        SupportTargetDef.OnBuffDef?.RemoveListener(GetDef);
        
        currentHealth = maxHealth;
        currentDef = maxDef;
        isEmptyHP = false;
    }
}