using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnNPCController : MonoBehaviour
{
    public Transform[] pointsToSpawn;
    public int maxRange;
    public GameObject[] npc;
    public int timeToSpawn;
    private int numberOfNPC;
    private int numberID;
    private bool isTurnOn;


    private void Update()
    {
        if (GameManager.Instance != null)
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                isTurnOn = true;
                StartCoroutine(StartSpawn());
            }
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            numberOfNPC = GameObject.FindGameObjectsWithTag("NPC").Length;
            if (numberOfNPC >= maxRange)
            {
                var coroutineCheckNumberOfNPC = StartCoroutine(CheckNumberOfNPC());
                yield return new WaitUntil(() => numberOfNPC < maxRange);
                StopCoroutine(coroutineCheckNumberOfNPC);
            }
            else
            {
                yield return new WaitForSeconds(timeToSpawn);
                SetRandomSpawn();
            }
        }
    }

    IEnumerator CheckNumberOfNPC()
    {
        while (true)
        {
            numberOfNPC = GameObject.FindGameObjectsWithTag("NPC").Length;
            yield return null;
        }
    }

    private void SetRandomSpawn()
    {
        var indexPoint = Random.Range(0, pointsToSpawn.Length);
        var indexRandomNpc = Random.Range(0, npc.Length);

        if (pointsToSpawn[indexPoint].name.Contains("PointAlly"))
        {
            if (pointsToSpawn[indexPoint].transform.childCount <= 0)
            {
                var newNPc = Instantiate(npc[indexRandomNpc], pointsToSpawn[indexPoint].transform.position,
                    Quaternion.identity,
                    pointsToSpawn[indexPoint].transform);
                var mainBody = newNPc.transform.GetChild(0);
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    //mask active
                    mainBody.transform.GetChild(2).gameObject.SetActive(true);
                    if (mainBody.transform.parent.parent.name.Contains("PointAlly"))
                    {
                        //set default when npc ally spawn 
                        mainBody.gameObject.layer = LayerMask.NameToLayer("Default");
                        mainBody.transform.GetChild(0).gameObject.SetActive(false);
                        mainBody.transform.GetChild(1).gameObject.SetActive(true);
                        mainBody.GetComponent<NPCDetectVirus>().enabled = false;
                        // mainBody.GetComponent<Animator>().SetBool("isIdle",true);
                        // mainBody.GetComponent<Animator>().SetBool("isWalk",false);
                        mainBody.GetComponent<FollowTarget>().enabled = false;
                        mainBody.GetComponent<NavMeshAgent>().enabled = false;
                        mainBody.GetComponent<NPCPatrolRandom>().enabled = false;
                        mainBody.GetComponent<NPCAnimationsController>().enabled = false;
                    }
                }
            }
        }
        else
        {
            var newNPc = Instantiate(npc[indexRandomNpc], pointsToSpawn[indexPoint].transform.position,
                Quaternion.identity,
                pointsToSpawn[indexPoint].transform.parent);
            numberID++;
            newNPc.transform.GetChild(0).name = newNPc.transform.GetChild(0).name + numberID;
            var mainBody = newNPc.transform.GetChild(0);
            if (SceneManager.GetActiveScene().buildIndex == 4)
                mainBody.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}