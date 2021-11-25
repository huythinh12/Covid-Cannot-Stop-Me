using System.Collections;
using UnityEngine;

public class SpawnNPCController : MonoBehaviour
{
    public Transform[] pointsToSpawn;
    public int maxRange;
    public GameObject[] npc;
    public int timeToSpawn;
    private int numberOfNPC;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawn());
        print("xin chao ");
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
                SetRandomSpawn();
                yield return new WaitForSeconds(timeToSpawn);
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
        Instantiate(npc[indexRandomNpc], pointsToSpawn[indexPoint].transform.position, Quaternion.identity,pointsToSpawn[indexPoint].transform.parent);
    }
}