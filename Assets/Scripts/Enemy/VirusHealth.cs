using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class VirusHealth : MonoBehaviour
{
    [HideInInspector] public UnityEvent UnityEvent_OnDeath;
    public int maxHealth;
    public GameObject particleEffect;
    private NavMeshAgent agent;
    private BehaviorTree behaviorTree;
    private SphereCollider collider;
    public int Health { get; set; }


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviorTree = GetComponent<BehaviorTree>();
        collider = GetComponent<SphereCollider>();

        Health = maxHealth;
    }

    public void GetHit()
    {
        Health--;
        if (Health <= 0)
        {
            agent.enabled = false;
            behaviorTree.enabled = false;
            collider.enabled = false;
            UnityEvent_OnDeath?.Invoke();
            Invoke("Dying", 1);
        }
        else
        {
            StartCoroutine(SlowDown());
        }
    }

    private IEnumerator SlowDown()
    {
        behaviorTree.enabled = false;
        yield return new WaitForSeconds(0.6f);
        behaviorTree.enabled = true;
    }

    private void Dying()
    {
        float timeDestroy = 3f;
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        if (transform != null)
            transform.DOMoveY(-2, timeDestroy);
        Destroy(gameObject, timeDestroy);
    }
}