using System.Collections;
using UnityEngine;

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
        var newNPc = Instantiate(npc[indexRandomNpc], pointsToSpawn[indexPoint].transform.position, Quaternion.identity,
            pointsToSpawn[indexPoint].transform.parent);
        numberID++;
        newNPc.transform.GetChild(0).name =  newNPc.transform.GetChild(0).name + numberID;
    }
}