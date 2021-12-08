using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform eyes;
    public int healBoss;
    private Animator anim;
    private int layerGetHit;
    private int getHit;
    private int layerIndexDeath;
    private int death;
    private bool isTurnOn;

    private void Awake()
    {
        healBoss = BossState.health;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        layerGetHit = anim.GetLayerIndex("GetHit");
        getHit = Animator.StringToHash("getHit");
        death = Animator.StringToHash("death");
        layerIndexDeath = anim.GetLayerIndex("Death");
        anim.SetLayerWeight(layerGetHit, 1);
        StartCoroutine(DetectColliderHit());
    }

    private void Update()
    {
        healBoss = BossState.health;

    }

    IEnumerator DetectColliderHit()
    {
        while (true)
        {
            yield return new WaitUntil(() => BossState.isGetHit);
            BossState.isGetHit = false;
            
            if (BossState.health <= 0)
            {
                anim.SetLayerWeight(layerIndexDeath, 1);
                anim.SetTrigger(death);
                StartCoroutine(Dying());
                break;
            }
            anim.SetTrigger(getHit);
        }
    }

  

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !BossState.isGetHit)
        {
            transform.DOLookAt(other.transform.position, 2);
            eyes.DOLookAt(other.transform.position, 2);

        }
    }

    IEnumerator Dying()
    {
        float timeDestroy = 2.5f;
        if (transform != null)
            transform.DOMoveY(-2, timeDestroy);
        Destroy(gameObject, timeDestroy);
        yield return new WaitForSeconds(1.8f);
        Mission4Controller.isBossDead = true;
    }
}