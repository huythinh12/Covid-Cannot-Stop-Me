using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem healEffect, defEffect;

    //show to edit 
    public int maxHealth;
    public int maxDef;
    public static UnityEvent<int, ParticleSystem> OnGetHitDef = new UnityEvent<int, ParticleSystem>();
    public static UnityEvent<int, ParticleSystem> OnGetHitHeal = new UnityEvent<int, ParticleSystem>();
    public static bool isEmptyHP;
    public static int currentHealth;
    public static int currentDef;

    private Slider health, def;

    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
        def = GameObject.FindGameObjectWithTag("Def").GetComponent<Slider>();
        if (health != null)
        {
            healEffect.Stop();
            defEffect.Stop();
        }
      
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

        isEmptyHP = false;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentDef--;
            OnGetHitDef?.Invoke(currentDef, defEffect);
            def.value = currentDef;
            health.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            SupportTarget.OnHealing?.RemoveListener(GetHealing);
            SupportTargetDef.OnBuffDef?.RemoveListener(GetDef);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Virus"))
        {
            if (currentDef > 0)
            {
                currentDef--;
                OnGetHitDef?.Invoke(currentDef, defEffect);
                if (def != null)
                    def.value = currentDef;
            }
            else
            {
                if (currentHealth > 0)
                {
                    currentHealth--;
                    OnGetHitHeal?.Invoke(currentHealth, healEffect);
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

    // delay because dependence from animation controller
    private void GetHealing(ParticleSystem healEffect)
    {
        Invoke("BuffHealDelay", 1.5f);
    }

    private void GetDef(ParticleSystem defEffect)
    {
        Invoke("BuffDefDelay", 1.5f);
    }

    private void BuffDefDelay()
    {
        currentDef++;
        def.value = currentDef;
    }

    private void BuffHealDelay()
    {
        currentHealth++;
        health.value = currentHealth;
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